using CSharpFunctionalExtensions;
using EShop.Application.Features.Commands.Categories.Create;
using EShop.Application.Features.Commands.Categories.Delete;
using EShop.Application.Features.Commands.Categories.Update;
using EShop.Application.Features.Queries.Categories.ById;
using EShop.Application.Features.Queries.Categories.SelectList;
using EShop.Application.Models;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;

public class CategoriesController : BaseController
{
    public CategoriesController(IMediator mediator) : base(mediator)
    {
    }


    [HttpGet("select-list")]
    public async Task<ActionResult<IEnumerable<SelectListItem<int>>>> GetSelectList(CancellationToken cancellationToken)
    {
        var categories = await Mediator.Send(new GetCategorySelectListQuery(), cancellationToken);

        return Ok(categories);
    }


    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Category>> GetById(int id, CancellationToken cancellationToken)
    {
        var category = await Mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);

        return Ok(category);
    }


    [HttpPost("create")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<bool>> Create(string name, CancellationToken cancellationToken)
    {
        var id = await Mediator.Send(new CreateCategoryCommand(name), cancellationToken);

        return StatusCode(StatusCodes.Status201Created, id);
    }


    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<int>>> Update(
        int id,
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UpdateCategoryCommand(id, name), cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status200OK, result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await Mediator.Send(new DeleteCategoryCommand(id), cancellationToken);

        return StatusCode(StatusCodes.Status204NoContent, deleted);
    }
}
