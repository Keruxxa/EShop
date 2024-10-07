using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Product;
using EShop.Application.CQRS.Commands.Products;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EShop.Application.CQRS.Queries.Products;
using EShop.Application.Issues.Errors.Base;

namespace EShop.Web.Controllers;

[AllowAnonymous]
public class ProductsController : BaseController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }



    [HttpGet]
    [Route("list")]
    public async Task<IEnumerable<ProductListItemDto>> GetList(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetProductListQuery(), cancellationToken);
    }


    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Result<ProductDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }


    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<Guid>>> Create(
        [FromBody] CreateProductDto createProductDto,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(createProductDto.Adapt<CreateProductCommand>(), cancellationToken);

        if (result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status201Created, result.Value);
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.Duplicate => Conflict(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest()
        };
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

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.Duplicate => Conflict(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest()
        };
    }


    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteProductCommand(id), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest()
        };
    }
}
