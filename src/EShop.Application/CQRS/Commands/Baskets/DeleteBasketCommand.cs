using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Baskets;

/// <summary>
///     Представляет команду для удаления корзины
/// </summary>
public record DeleteBasketCommand(Guid Id) : IRequest<Result<Unit, Error>>;
