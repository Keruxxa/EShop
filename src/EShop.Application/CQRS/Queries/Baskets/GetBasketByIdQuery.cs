using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Basket;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Queries.Baskets;

/// <summary>
///     Представляет запрос для получения корзины по Id
/// </summary>
public record GetBasketByIdQuery(Guid Id) : IRequest<Result<BasketDto, Error>>;
