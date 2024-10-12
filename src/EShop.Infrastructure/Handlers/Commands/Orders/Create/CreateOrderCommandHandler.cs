using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Orders;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Orders.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateOrderCommand"/>
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Guid, Error>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductService _productService;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IProductService productService)
    {
        _orderRepository = orderRepository;
        _productService = productService;
    }


    public async Task<Result<Guid, Error>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var productIds = request.СreateOrderDto.CreateOrderItemDtos.Select(itemDto => itemDto.ProductId);

        if (!await _productService.IsAllProductsExistAsync(productIds, cancellationToken))
        {
            return Result.Failure<Guid, Error>(new Error(new BadRequestEntityError(ORDER_ITEM_DOES_NOT_EXIST), ErrorType.BadRequest));
        }

        var order = new Order(request.СreateOrderDto.UserId);

        foreach (var item in request.СreateOrderDto.CreateOrderItemDtos)
        {
            var orderItem = new OrderItem(order.Id, item.ProductId, item.Count);

            order.AddOrderItem(orderItem);
        }

        _orderRepository.Create(order);

        var isSaved = await _orderRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Guid, Error>(order.Id)
            : Result.Failure<Guid, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
