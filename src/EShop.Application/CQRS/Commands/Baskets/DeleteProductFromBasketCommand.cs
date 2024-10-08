using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Baskets;

/// <summary>
///     Представляет команду для удаления товара из корзины
/// </summary>
public record DeleteProductFromBasketCommand(Guid BasketId, Guid ProductId) : IRequest<Result<Unit, Error>>;
