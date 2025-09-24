using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Products;
using EShop.Application.Dtos.Product;
using EShop.Application.Dtos.ProductImages;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using EShop.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Queries.Products.ById;

/// <summary>
///     Представялет обработчик запроса <see cref="GetProductByIdQuery"/>
/// </summary>
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto, Error>>
{
    private readonly EShopDbContext _dbContext;

    public GetProductByIdQueryHandler(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result<ProductDto, Error>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productDto = await _dbContext.Products
            .Where(p => p.Id == request.Id)
            .Select(p => new ProductDto(
                p.Id, p.Name,
                p.Price,
                p.Category!.Name,
                p.Images.Select(i => new ProductImageDto(i.Uri, i.IsMain, i.Order)),
                p.CountryManufacturer!.Name,
                p.Description,
                p.ReleaseDate))
            .FirstOrDefaultAsync(cancellationToken);

        if (productDto is null)
        {
            return Result.Failure<ProductDto, Error>(new Error(new NotFoundEntityError(nameof(Product), request.Id), ErrorType.NotFound));
        }

        return Result.Success<ProductDto, Error>(productDto);
    }
}
