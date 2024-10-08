﻿using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Products;
using EShop.Application.Dtos.Product;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Queries.Products.ById;

/// <summary>
///     Представялет обработчик запроса <see cref="GetProductByIdQuery"/>
/// </summary>
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IEShopDbContext _dbContext;

    public GetProductByIdQueryHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .Include(product => product.Category)
            .Include(product => product.CountryManufacturer)
            .FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        return Result.Success(product.Adapt<ProductDto>());
    }
}

