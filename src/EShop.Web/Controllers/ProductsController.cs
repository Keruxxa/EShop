using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Products;
using EShop.Application.CQRS.Queries.Products;
using EShop.Application.Dtos.Product;
using EShop.Application.Dtos.ProductImages;
using EShop.Application.Issues.Errors.Base;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EShop.Web.Controllers;

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
    public async Task<ActionResult<Result<ProductDto, Error>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }


    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<Guid, Error>>> Create([FromForm] CreateProductDto createProductDto, CancellationToken cancellationToken)
    {
        var createProductCommandConfig = GetProductCommandConfig(createProductDto.Images);

        var result = await Mediator.Send(createProductDto.Adapt<CreateProductCommand>(createProductCommandConfig), cancellationToken);

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
    public async Task<ActionResult<Result<Unit, Error>>> Update(
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
    public async Task<ActionResult<Result<Unit, Error>>> Delete(Guid id, CancellationToken cancellationToken)
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


    private TypeAdapterConfig GetProductCommandConfig(IEnumerable<IFormFile> Images)
    {
        return new TypeAdapterConfig()
            .NewConfig<CreateProductDto, CreateProductCommand>()
            .ConstructUsing(src => new CreateProductCommand(
                src.Name,
                src.CategoryId,
                src.BrandId,
                src.Price,
                Images,
                DeserializeImagesInfo(src.ImagesInfo),
                src.Description,
                src.ReleaseDate,
                src.CountryManufacturerId))
            .Config;
    }

    private static IEnumerable<CreateProductImageInfo> DeserializeImagesInfo(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            throw new BadHttpRequestException($"{nameof(CreateProductDto.ImagesInfo)} must have value");
        }

        var imagesInfo = JsonConvert.DeserializeObject<List<CreateProductImageInfo>>(json);

        if (imagesInfo is null)
        {
            throw new BadHttpRequestException($"{nameof(CreateProductDto.ImagesInfo)} must have value");
        }

        return imagesInfo;
    }
}
