using EShop.Application.CQRS.Queries.Products;
using EShop.Application.Dtos.Product;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Products.List;

/// <summary>
///     Представляет обработчик запроса <see cref="GetProductListQuery"/>
/// </summary>
public class GetProductListQueryHandler
    : IRequestHandler<GetProductListQuery, IEnumerable<ProductListItemDto>>
{
    private readonly IEShopDbContext _context;
    private readonly IProductRepository _productRepository;

    public GetProductListQueryHandler(IEShopDbContext context, IProductRepository productRepository)
    {
        _context = context;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductListItemDto>> Handle(
        GetProductListQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetListAsync(cancellationToken);

        return products.Select(product =>
        {
            return product.Adapt<ProductListItemDto>();
        });
    }
}
