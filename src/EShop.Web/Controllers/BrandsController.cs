﻿using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.CQRS.Queries.Brands;
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
    public async Task<ActionResult<IEnumerable<SelectListItem<int>>>> GetSelectList(CancellationToken cancellationToken)
    {
        var brands = await Mediator.Send(new GetBrandSelectListQuery(), cancellationToken);

        return StatusCode(StatusCodes.Status200OK, brands);
    }


    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Brand>> GetById(int id, CancellationToken cancellationToken)
    {
        var brand = await Mediator.Send(new GetBrandByIdQuery(id), cancellationToken);

        return StatusCode(StatusCodes.Status200OK, brand);
    }


    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<int>> Create([FromQuery] string name, CancellationToken cancellationToken)
    {
        var id = await Mediator.Send(new CreateBrandCommand(name), cancellationToken);

        return StatusCode(StatusCodes.Status201Created, id);
    }


    [HttpPatch("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<int>>> Update(
        int id,
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UpdateBrandCommand(id, name), cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status200OK, result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await Mediator.Send(new DeleteBrandCommand(id), cancellationToken);

        return StatusCode(StatusCodes.Status204NoContent, deleted);
    }
}
