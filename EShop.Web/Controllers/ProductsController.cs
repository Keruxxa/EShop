using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Product;
using EShop.Application.Features.Commands.Products.Create;
using EShop.Application.Features.Commands.Products.Delete;
using EShop.Application.Features.Commands.Products.Update;
using EShop.Application.Features.Queries.Products.ById;
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
            var productDto = await Mediator.Send(new GetProductByIdQuery(id));

            return StatusCode(StatusCodes.Status200OK, productDto);
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductDto createProductDto)
        {
            var createCommand = createProductDto.Adapt<CreateProductCommand>();

            var createResult = await Mediator.Send(createCommand);

            return createResult.IsSuccess
                ? RedirectToAction(nameof(GetById), new { id = createResult.Value })
                : StatusCode(StatusCodes.Status500InternalServerError, createResult.Error);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Result>> Update([FromBody] UpdateProductDto updateProductDto)
        {
            var updateCommand = updateProductDto.Adapt<UpdateProductCommand>();

            var updateResult = await Mediator.Send(updateCommand);

            return updateResult.IsSuccess
                ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status500InternalServerError, updateResult.Error);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var isSuccessDeleted = await Mediator.Send(new DeleteProductCommand(id));

            return StatusCode(StatusCodes.Status204NoContent, isSuccessDeleted);
        }
    }
}
