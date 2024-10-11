using EShop.Application.CQRS.Commands.Orders;
using EShop.Application.CQRS.Queries.Orders;
using EShop.Application.Dtos.Orders;
using EShop.Application.Issues.Errors.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;

public class OrderController : BaseController
{
    public OrderController(IMediator mediator) : base(mediator)
    {
    }



    [HttpGet("list")]
    public async Task<ActionResult> GetListAsync(Guid userId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetOrderListQuery(userId), cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            _ => BadRequest(error)
        };
    }


    [HttpGet("{id:Guid}")]
    public async Task<ActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetOrderByIdQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }


    [HttpPost]
    public async Task<ActionResult> CreateOrderAsync([FromBody] CreateOrderDto createOrderDto, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new CreateOrderCommand(createOrderDto), cancellationToken);

        if (result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status201Created, result.Value);
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.BadRequest => BadRequest(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest(error)
        };

        throw new NotImplementedException();
    }
}
