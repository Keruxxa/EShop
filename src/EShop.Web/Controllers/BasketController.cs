using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Baskets;
using EShop.Application.CQRS.Queries.Baskets;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;

public class BasketController : BaseController
{
    public BasketController(IMediator mediator) : base(mediator)
    {
    }


    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Result<Basket, Error>>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBasketByIdQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }


    [HttpPost("add-product")]
    public async Task<ActionResult<Result<Unit, Error>>> AddProductAsync(
        [FromQuery] Guid basketId,
        [FromQuery] Guid productId,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new AddProductToBasketCommand(basketId, productId), cancellationToken);

        if (result.IsSuccess)
        {
            return Created();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest(error)
        };
    }


    [HttpDelete("delete-product")]
    public async Task<ActionResult<Result<Unit, Error>>> DeleteProductAsync(Guid basketId, Guid productId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteProductFromBasketCommand(basketId, productId), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest(error)
        };
    }


    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<Result<Unit, Error>>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteBasketCommand(id), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest(error)
        };
    }
}
