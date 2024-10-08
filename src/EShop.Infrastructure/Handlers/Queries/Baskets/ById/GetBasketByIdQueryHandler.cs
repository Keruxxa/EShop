using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Baskets;
using EShop.Application.Dtos.Basket;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Baskets.ById;

/// <summary>
///     Представляет обработчик запроса <see cref="GetBasketByIdQuery"/>
/// </summary>
public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQuery, Result<BasketDto, Error>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public GetBasketByIdQueryHandler(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }


    public async Task<Result<BasketDto, Error>> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetByIdAsync(request.Id, cancellationToken);

        if (basket is null)
        {
            return Result.Failure<BasketDto, Error>(new Error(new NotFoundEntityError(nameof(Basket), request.Id), ErrorType.NotFound));
        }

        var basketDto = _mapper.From(basket).AdaptToType<BasketDto>();

        return Result.Success<BasketDto, Error>(basketDto);
    }
}
