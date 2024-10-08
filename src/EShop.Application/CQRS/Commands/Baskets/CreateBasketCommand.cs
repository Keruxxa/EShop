using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Baskets;

/// <summary>
///     Представляет команду для создания корзины
/// </summary>
public record CreateBasketCommand(Guid Id) : IRequest<Result<Guid, Error>>;
