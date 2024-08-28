using CSharpFunctionalExtensions;
using EShop.Application.Features.Commands.Countries.Create;
using EShop.Application.Features.Commands.Countries.Delete;
using EShop.Application.Features.Commands.Countries.Update;
using EShop.Application.Features.Queries.Countries.ById;
using EShop.Application.Features.Queries.Countries.SelectList;
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
    public async Task<ActionResult<IEnumerable<SelectListItem<int>>>> GetSelectList(CancellationToken cancellationToken)
    {
        var getListQuery = await Mediator.Send(new GetCountriesSelectListQuery(), cancellationToken);

        return StatusCode(StatusCodes.Status200OK, getListQuery);
    }


    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Country>> GetById(int id, CancellationToken cancellationToken)
    {
        var country = await Mediator.Send(new GetCountryByIdQuery(id), cancellationToken);

        return StatusCode(StatusCodes.Status200OK, country);
    }


    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<int>> Create(string name, CancellationToken cancellationToken)
    {
        var id = await Mediator.Send(new CreateCountryCommand(name), cancellationToken);

        return StatusCode(StatusCodes.Status201Created, id);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result<int>>> Update(
        int id,
        [FromRoute] string name,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UpdateCountryCommand(id, name), cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status200OK, result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        var deleted = await Mediator.Send(new DeleteCountryCommand(id), cancellationToken);

        return StatusCode(StatusCodes.Status204NoContent, deleted);
    }
}
