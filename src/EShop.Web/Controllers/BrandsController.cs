using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.CQRS.Queries.Brands;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Models;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;

public class BrandsController : BaseController
{
    public BrandsController(IMediator mediator) : base(mediator)
    {
    }


    [HttpGet("select-list")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<SelectListItem<int>>>> GetSelectList(CancellationToken cancellationToken)
    {
        var brands = await Mediator.Send(new GetBrandSelectListQuery(), cancellationToken);

        return Ok(brands);
    }


    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<Brand, Error>>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBrandByIdQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }


    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<int, Error>>> Create([FromQuery] string name, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new CreateBrandCommand(name), cancellationToken);

        if (result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status201Created, result.Value);
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.Duplicate => Conflict(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest()
        };
    }


    [HttpPatch("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result>> Update(
        int id,
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UpdateBrandCommand(id, name), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return result.Error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.Duplicate => Conflict(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest()
        };
    }


    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteBrandCommand(id), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return result.Error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest()
        };
    }
}
