using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Favorites;
using EShop.Application.CQRS.Queries.Favorites;
using EShop.Application.Issues.Errors.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;

public class FavoriteController : BaseController
{
    public FavoriteController(IMediator mediator) : base(mediator)
    {
    }


    [HttpGet("list")]
    public async Task<ActionResult> GetListAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetFavoriteByIdQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }


    [HttpPost("add-product")]
    public async Task<ActionResult<Result<Unit, Error>>> AddProductAsync(
        [FromQuery] Guid id,
        [FromQuery] Guid ProductId,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new AddProductToFavoriteCommand(id, ProductId), cancellationToken);

        if (result.IsSuccess)
        {
            return Created();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            _ => BadRequest(error)
        };
    }


    [HttpDelete("delete-product")]
    public async Task<ActionResult<Result<Unit, Error>>> DeleteProductAsync(
        [FromQuery] Guid id,
        [FromQuery] Guid productId,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteProductFromFavoriteCommand(id, productId), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            _ => BadRequest(error)
        };
    }


    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<Result<Unit, Error>>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteFavoriteCommand(id));

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            _ => BadRequest(error)
        };
    }
}
