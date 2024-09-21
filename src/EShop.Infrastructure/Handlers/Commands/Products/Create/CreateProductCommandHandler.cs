using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Products;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
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
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IEShopDbContext dbContext, IProductRepository productRepository)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
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

        _productRepository.Create(product);

        var saved = await _productRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success(product.Id)
            : Result.Failure<Guid>(SERVER_SIDE_ERROR);
    }
}

