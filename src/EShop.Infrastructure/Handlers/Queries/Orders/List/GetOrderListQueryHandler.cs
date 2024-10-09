using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Orders;
using EShop.Application.Dtos.Orders;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Orders.List;

/// <summary>
///     Представляет обработчик запроса <see cref="GetOrderListQuery"/>
/// </summary>
public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, Result<List<OrderDto>, Error>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetOrderListQueryHandler(IOrderRepository orderRepository, IUserService userService, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _userService = userService;
        _mapper = mapper;
    }


    public async Task<Result<List<OrderDto>, Error>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        if (!await _userService.IsUserExistAsync(request.UserId, cancellationToken))
        {
            return Result.Failure<List<OrderDto>, Error>(new Error(new NotFoundEntityError(nameof(User), request.UserId), ErrorType.NotFound));
        }

        var orders = await _orderRepository.GetListByUserIdAsync(request.UserId, cancellationToken);

        return Result.Success<List<OrderDto>, Error>(orders.Select(order =>
        {
            return _mapper.From(order).AdaptToType<OrderDto>();
        }).ToList());
    }
}
