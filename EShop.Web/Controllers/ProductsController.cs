using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Product;
using EShop.Application.Features.Commands.Products.Create;
using EShop.Application.Features.Commands.Products.Delete;
using EShop.Application.Features.Commands.Products.Update;
using EShop.Application.Features.Queries.Products.ById;
using EShop.Application.Features.Queries.Products.List;
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
        public async Task<ActionResult<Result<ProductDto>>> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetProductByIdQuery(id));

            return result.IsSuccess
                ? StatusCode(StatusCodes.Status200OK, result.Value)
                : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
        }


        [HttpGet]
        [Route("list")]
        public async Task<IEnumerable<ProductListItemDto>> GetList()
        {
            return await Mediator.Send(new GetProductListQuery());
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateProductDto createProductDto)
        {
            var createCommand = createProductDto.Adapt<CreateProductCommand>();

            var result = await Mediator.Send(createCommand);

            return result.IsSuccess
                ? RedirectToAction(nameof(GetById), new { id = result.Value })
                : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
        }


        [HttpPut("update")]
        public async Task<ActionResult<Result>> Update([FromBody] UpdateProductDto updateProductDto)
        {
            var updateCommand = updateProductDto.Adapt<UpdateProductCommand>();

            var result = await Mediator.Send(updateCommand);

            return result.IsSuccess
                ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
        }


        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Result<bool>>> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteProductCommand(id));

            return result.IsSuccess
                ? StatusCode(StatusCodes.Status204NoContent, result.Value)
                : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
        }
    }
}
