using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Countries;
using EShop.Application.CQRS.Queries.Countries;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Models;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;

[Authorize]
public class CountriesController : BaseController
{
    public CountriesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("select-list")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<SelectListItem<int>>>> GetSelectList(CancellationToken cancellationToken)
    {
        var getListQuery = await Mediator.Send(new GetCountriesSelectListQuery(), cancellationToken);

        return StatusCode(StatusCodes.Status200OK, getListQuery);
    }


    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<Country, Error>>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCountryByIdQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }


    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<int, Error>>> Create([FromQuery] string name, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new CreateCountryCommand(name), cancellationToken);

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
    public async Task<ActionResult<Result<Unit, Error>>> Update(
        int id,
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UpdateCountryCommand(id, name), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.Duplicate => Conflict(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest()
        };
    }


    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<Unit, Error>>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteCountryCommand(id), cancellationToken);

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
}
