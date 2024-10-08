using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Baskets;

/// <summary>
///     Представляет команду для добавления товара в корзину
/// </summary>
public record class AddProductToBasketCommand(Guid BasketId, Guid ProductId) : IRequest<Result<Unit, Error>>;
