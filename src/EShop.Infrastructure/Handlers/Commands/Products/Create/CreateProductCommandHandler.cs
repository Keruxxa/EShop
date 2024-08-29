using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Products;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Products.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateProductCommand"/>
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IEShopDbContext _dbContext;

    public CreateProductCommandHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productExists = await _dbContext.Products
            .AnyAsync(product => product.Name.Equals(request.Name));

        if (productExists)
        {
            throw new DuplicateEntityException(nameof(Product));
        }

        var product = new Product(
            request.Name,
            request.Description,
            request.ReleaseDate,
            request.Price,
            request.CategoryId,
            request.BrandId,
            request.CountryManufacturerId)
        {
            Id = Guid.NewGuid()
        };

        _dbContext.Products.Add(product);
        _dbContext.BrandProducts.Add(new(request.BrandId, product.Id));

        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success(product.Id)
            : Result.Failure<Guid>(SERVER_SIDE_ERROR);
    }
}

