using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Products;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;

namespace EShop.Infrastructure.Handlers.Commands.Products.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateProductCommand"/>
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IEShopDbContext dbContext, IProductRepository productRepository)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
    }


    public async Task<Result<Guid, Error>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productExists = await _dbContext.Products
            .AnyAsync(product => product.Name.Equals(request.Name));

        if (productExists)
        {
            return Result.Failure<Guid, Error>(new Error(new DuplicateEntityError(nameof(Product)), ErrorType.Duplicate));
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
            ? Result.Success<Guid, Error>(product.Id)
            : Result.Failure<Guid, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}

