using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Reviews;
using EShop.Application.CQRS.Queries.Reviews;
using EShop.Application.Dtos.Review;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;

[Authorize(Roles = "RegisteredUser")]
public class ReviewController : BaseController
{
    public ReviewController(IMediator mediator) : base(mediator)
    {
    }

    [AllowAnonymous]
    [HttpGet("list-by-product-id")]
    public async Task<ActionResult<Result<List<Review>, Error>>> GetListByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetReviewListByProductIdQuery(productId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }


    [HttpGet("list-by-user-id")]
    public async Task<ActionResult<Result<List<Review>, Error>>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetReviewListByUserIdQuery(userId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }


    [HttpPost]
    public async Task<ActionResult<Result<Review, Error>>> CreateAsync([FromBody] CreateReviewDto createReviewDto, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(createReviewDto.Adapt<CreateReviewCommand>(), cancellationToken);

        if (result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status201Created, result.Value);
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            _ => BadRequest(error)
        };
    }


    [HttpPatch]
    public async Task<ActionResult<Result<Unit, Error>>> UpdateAsync(
        [FromBody] UpdateReviewDto updateReviewDto,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(updateReviewDto.Adapt<UpdateReviewCommand>(), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest()
        };
    }


    [HttpDelete]
    public async Task<ActionResult<Result<Unit, Error>>> DeleteAsync(
        Guid productId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteReviewCommand(productId, userId), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.Forbidden => StatusCode(StatusCodes.Status403Forbidden, error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest()
        };
    }
}
