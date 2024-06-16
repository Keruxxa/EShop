using EShop.Application.Dtos.Product;
using EShop.Application.Features.Commands.Products;
using EShop.Application.Features.Queries.Products;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var productDto = await Mediator.Send(new GetProductByIdQuery(id));

            return StatusCode(StatusCodes.Status200OK, productDto);
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductDto createProductDto)
        {
            var createCommand = createProductDto.Adapt<CreateProductCommand>();

            var productResult = await Mediator.Send(createCommand);

            return productResult.IsSuccess
                ? RedirectToAction(nameof(GetById), new { id = productResult.Value })
                : StatusCode(StatusCodes.Status500InternalServerError, productResult.Error);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var deleted = await Mediator.Send(new DeleteProductCommand(id));

            return StatusCode(StatusCodes.Status204NoContent, deleted);
        }
    }
}
