using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Products;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;

namespace EShop.Infrastructure.Handlers.Commands.Products.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateProductCommand"/>
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Unit, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IEShopDbContext dbContext, IProductRepository productRepository)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
    }


    public async Task<Result<Unit, Error>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Product), request.Id), ErrorType.NotFound));
        }

        var productExists = await _dbContext.Products
            .AnyAsync(product => product.Name.Equals(request.Name));

        if (productExists)
        {
            return Result.Failure<Unit, Error>(new Error(new DuplicateEntityError(nameof(Product)), ErrorType.Duplicate));
        }

        product.UpdateEntity(request.Name, request.Description, request.ReleaseDate,
            request.Price, request.CategoryId, request.BrandId, request.CountryManufacturerId);

        _productRepository.Update(product);

        var saved = await _productRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
