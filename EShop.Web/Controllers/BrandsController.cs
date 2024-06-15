using EShop.Application.Features.Commands.Brands;
using EShop.Application.Features.Models;
using EShop.Application.Features.Queries.Brands;
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


        [HttpPost]
        public async Task<ActionResult<int>> Create(string name)
        {
            if (string.IsNullOrEmpty(name?.Trim()))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var id = await Mediator.Send(new CreateBrandCommand(name));

            return StatusCode(StatusCodes.Status201Created, id);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (id < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var deleteCommand = new DeleteBrandCommand
            {
                Id = id
            };

            var deleted = await Mediator.Send(deleteCommand);

            return StatusCode(StatusCodes.Status204NoContent, deleted);
        }
    }
}
