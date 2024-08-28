using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Product;
using EShop.Application.Features.Commands.Products.Create;
using EShop.Application.Features.Commands.Products.Delete;
using EShop.Application.Features.Commands.Products.Update;
using EShop.Application.Features.Queries.Products.ById;
using EShop.Application.Features.Queries.Products.List;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;

[AllowAnonymous]
public class ProductsController : BaseController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }


    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Result<ProductDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(id), cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status200OK, result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpGet]
    [Route("list")]
    public async Task<IEnumerable<ProductListItemDto>> GetList(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetProductListQuery(), cancellationToken);
    }


    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<Guid>>> Create(
        [FromBody] CreateProductDto createProductDto,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(createProductDto.Adapt<CreateProductCommand>(), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpPatch("{id:Guid}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result>> Update(
        Guid id,
        [FromBody] UpdateProductDto updateProductDto,
        CancellationToken cancellationToken)
    {
        updateProductDto.Id = id;

        var result = await Mediator.Send(updateProductDto.Adapt<UpdateProductCommand>(), cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteProductCommand(id), cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }
}
