using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Orders;
using EShop.Application.Dtos.Orders;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Orders.ById;

/// <summary>
///     Представляет обработчик запроса <see cref="GetOrderByIdQuery"/>
/// </summary>
public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderDto, Error>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }


    public async Task<Result<OrderDto, Error>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

        if (order is null)
        {
            return Result.Failure<OrderDto, Error>(new Error(new NotFoundEntityError(nameof(Order), request.Id), ErrorType.NotFound));
        }

        var orderDto = _mapper.From(order).AdaptToType<OrderDto>();

        return Result.Success<OrderDto, Error>(orderDto);
    }
}
