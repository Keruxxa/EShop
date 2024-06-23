using CSharpFunctionalExtensions;
using EShop.Application.Features.Commands.Categories.Create;
using EShop.Application.Features.Commands.Categories.Delete;
using EShop.Application.Features.Commands.Categories.Update;
using EShop.Application.Features.Queries.Categories.ById;
using EShop.Application.Features.Queries.Categories.SelectList;
using EShop.Application.Models;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet("select-list")]
        public async Task<ActionResult<IEnumerable<SelectListItem<int>>>> GetSelectList()
        {
            var categories = await Mediator.Send(new GetCategorySelectListQuery());

            return StatusCode(StatusCodes.Status200OK, categories);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await Mediator.Send(new GetCategoryByIdQuery(id));

            return StatusCode(StatusCodes.Status200OK, category);
        }


        [HttpPost("create")]
        public async Task<ActionResult<bool>> Create(string name)
        {
            var id = await Mediator.Send(new CreateCategoryCommand(name));

            return StatusCode(StatusCodes.Status201Created, id);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<Result<int>>> Update(int id, [FromQuery] string name)
        {
            var result = await Mediator.Send(new UpdateCategoryCommand(id, name));

            return result.IsSuccess
                ? StatusCode(StatusCodes.Status200OK, result.Value)
                : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var deleted = await Mediator.Send(new DeleteCategoryCommand(id));

            return StatusCode(StatusCodes.Status204NoContent, deleted);
        }
    }
}
