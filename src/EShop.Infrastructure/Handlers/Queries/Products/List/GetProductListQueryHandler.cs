using EShop.Application.CQRS.Queries.Products;
using EShop.Application.Dtos.Product;
using EShop.Application.Dtos.ProductImages;
using EShop.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Queries.Products.List;

/// <summary>
///     Представляет обработчик запроса <see cref="GetProductListQuery"/>
/// </summary>
public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IEnumerable<ProductListItemDto>>
{
    private readonly EShopDbContext _dbContext;

    public GetProductListQueryHandler(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<ProductListItemDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Products
            .Select(p => new ProductListItemDto(
                p.Id,
                p.Name,
                p.Price,
                p.ReviewCount,
                p.Images.Select(i => new ProductImageDto(i.Uri, i.IsMain, i.Order)),
                p.Description,
                p.ReleaseDate,
                p.Rating))
            .ToListAsync(cancellationToken);
    }
}
