using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Orders;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Queries.Orders;

/// <summary>
///     Представляет запрос для получения заказа по Id
/// </summary>
public record GetOrderByIdQuery(Guid Id) : IRequest<Result<OrderDto, Error>>;
