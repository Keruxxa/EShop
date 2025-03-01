using EShop.Application.CQRS.Queries.Products;
using EShop.Application.Dtos.Product;
using EShop.Application.Interfaces.Repositories;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Queries.Products.List;

/// <summary>
///     Представляет обработчик запроса <see cref="GetProductListQuery"/>
/// </summary>
public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IEnumerable<ProductListItemDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductListQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }


    public async Task<IEnumerable<ProductListItemDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var products = _productRepository
            .GetList()
            .Include(product => product.Reviews);

        var productListItemDtos = products.Select(product => product.Adapt<ProductListItemDto>());

        return await productListItemDtos.ToListAsync(cancellationToken);
    }
}
