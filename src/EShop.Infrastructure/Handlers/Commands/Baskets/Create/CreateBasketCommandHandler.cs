using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Baskets;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Infrastructure.Handlers.Commands.Baskets.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateBasketCommand"/>
/// </summary>
public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, Result<Guid, Error>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IUserService _userService;

    public CreateBasketCommandHandler(IBasketRepository basketRepository, IUserService userService)
    {
        _basketRepository = basketRepository;
        _userService = userService;
    }


    public async Task<Result<Guid, Error>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetByIdAsync(request.Id, cancellationToken);

        if (basket is not null)
        {
            return Result.Failure<Guid, Error>(new Error(new DuplicateEntityError(nameof(Basket)), ErrorType.Duplicate));
        }

        if (!await _userService.IsUserExistAsync(request.Id, cancellationToken))
        {
            return Result.Failure<Guid, Error>(new Error(new NotFoundEntityError(nameof(User), request.Id), ErrorType.Duplicate));
        }

        basket = new Basket(request.Id);

        var id = _basketRepository.Create(basket);

        var isSaved = await _basketRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Guid, Error>(id)
            : Result.Failure<Guid, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
