using EShop.Application.Features.Commands.Brands.Create;
using EShop.Application.Features.Commands.Brands.Delete;
using EShop.Application.Features.Models;
using EShop.Application.Features.Queries.Brands.ById;
using EShop.Application.Features.Queries.Brands.SelectList;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class BrandsController : BaseController
    {
        public BrandsController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet("select-list")]
        public async Task<ActionResult<IEnumerable<SelectListItem<int>>>> GetSelectList()
        {
            var brands = await Mediator.Send(new GetBrandSelectListQuery());

            return StatusCode(StatusCodes.Status200OK, brands);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Brand>> GetById(int id)
        {
            var brand = await Mediator.Send(new GetBrandByIdQuery(id));

            return StatusCode(StatusCodes.Status200OK, brand);
        }


        [HttpPost("create")]
        public async Task<ActionResult<int>> Create(string? name)
        {
            var id = await Mediator.Send(new CreateBrandCommand(name));

            return StatusCode(StatusCodes.Status201Created, id);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var deleted = await Mediator.Send(new DeleteBrandCommand(id));

            return StatusCode(StatusCodes.Status204NoContent, deleted);
        }
    }
}
