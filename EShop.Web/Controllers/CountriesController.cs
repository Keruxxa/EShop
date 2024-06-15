using EShop.Application.Features.Commands.Countries;
using EShop.Application.Features.Models;
using EShop.Application.Features.Queries.Countries;
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
            var getByIdQuery = new GetCountryByIdQuery()
            {
                Id = id
            };

            var country = await Mediator.Send(getByIdQuery);

            return StatusCode(StatusCodes.Status200OK, country);
        }


        [HttpPost("create")]
        public async Task<ActionResult<int>> Create(string name)
        {
            var createCommand = new CreateCountryCommand
            {
                Name = name
            };

            var id = await Mediator.Send(createCommand);

            return StatusCode(StatusCodes.Status201Created, id);
        }
    }
}
