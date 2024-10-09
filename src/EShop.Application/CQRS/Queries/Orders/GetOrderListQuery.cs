using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Orders;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Queries.Orders;

/// <summary>
///     Представляет запрос для получения списка заказов пользователя
/// </summary>
public record GetOrderListQuery(Guid UserId) : IRequest<Result<List<OrderDto>, Error>>;
