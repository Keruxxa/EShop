﻿using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Countries;
using EShop.Application.CQRS.Queries.Countries;
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

        return Ok(country);
    }


    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<int>> Create([FromQuery] string name, CancellationToken cancellationToken)
    {
        var id = await Mediator.Send(new CreateCountryCommand(name), cancellationToken);

        return StatusCode(StatusCodes.Status201Created, id);
    }


    [HttpPatch("{id:int}")]
    public async Task<ActionResult<Result>> Update(
        int id,
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UpdateCountryCommand(id, name), cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteCountryCommand(id), cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }
}
