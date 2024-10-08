﻿using EShop.Application.CQRS.Queries.Products;
using EShop.Application.Dtos.Product;
using EShop.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Queries.Products.List;

/// <summary>
///     Представляет обработчик запроса <see cref="GetProductListQuery"/>
/// </summary>
public class GetProductListQueryHandler
    : IRequestHandler<GetProductListQuery, IEnumerable<ProductListItemDto>>
{
    private readonly IEShopDbContext _context;

    public GetProductListQueryHandler(IEShopDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductListItemDto>> Handle(
        GetProductListQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return products.Select(product =>
        {
            return product.Adapt<ProductListItemDto>();
        });
    }
}
