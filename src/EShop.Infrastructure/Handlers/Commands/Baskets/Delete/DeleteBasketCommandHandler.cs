using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Baskets;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Infrastructure.Handlers.Commands.Baskets.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteBasketCommand"/>
/// </summary>
public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, Result<Unit, Error>>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteBasketCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }


    public async Task<Result<Unit, Error>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetByIdAsync(request.Id, cancellationToken);

        if (basket is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Basket), request.Id), ErrorType.NotFound));
        }

        _basketRepository.Delete(basket);

        var isSaved = await _basketRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
