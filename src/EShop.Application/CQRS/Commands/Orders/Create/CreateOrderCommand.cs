using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Orders;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Orders.Create;

/// <summary>
///     Представляет команду для создания заказа
/// </summary>
public record CreateOrderCommand(CreateOrderDto СreateOrderDto) : IRequest<Result<Guid, Error>>;
