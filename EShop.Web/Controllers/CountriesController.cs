using CSharpFunctionalExtensions;
using EShop.Application.Features.Commands.Countries.Create;
using EShop.Application.Features.Commands.Countries.Delete;
using EShop.Application.Features.Commands.Countries.Update;
using EShop.Application.Features.Models;
using EShop.Application.Features.Queries.Countries.ById;
using EShop.Application.Features.Queries.Countries.SelectList;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class CountriesController : BaseController
    {
        public CountriesController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet("select-list")]
        public async Task<ActionResult<IEnumerable<SelectListItem<int>>>> GetSelectList()
        {
            var getListQuery = await Mediator.Send(new GetCountriesSelectListQuery());

            return StatusCode(StatusCodes.Status200OK, getListQuery);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Country>> GetById(int id)
        {
            var country = await Mediator.Send(new GetCountryByIdQuery(id));

            return StatusCode(StatusCodes.Status200OK, country);
        }


        [HttpPost("create")]
        public async Task<ActionResult<int>> Create(string name)
        {
            var id = await Mediator.Send(new CreateCountryCommand(name));

            return StatusCode(StatusCodes.Status201Created, id);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<Result<int>>> Update(int id, [FromRoute] string name)
        {
            var result = await Mediator.Send(new UpdateCountryCommand(id, name));

            return result.IsSuccess
                ? StatusCode(StatusCodes.Status200OK, result.Value)
                : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var deleted = await Mediator.Send(new DeleteCountryCommand(id));

            return StatusCode(StatusCodes.Status204NoContent, deleted);
        }
    }
}
