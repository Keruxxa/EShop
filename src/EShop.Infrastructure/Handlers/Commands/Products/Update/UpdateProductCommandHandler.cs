using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Products;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Products.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateProductCommand"/>
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IEShopDbContext dbContext, IProductRepository productRepository)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
    }


    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        var productExists = await _dbContext.Products
            .AnyAsync(product => product.Name.Equals(request.Name));

        if (productExists)
        {
            throw new DuplicateEntityException(nameof(Product));
        }

        product.UpdateEntity(request.Name, request.Description, request.ReleaseDate,
            request.Price, request.CategoryId, request.BrandId, request.CountryManufacturerId);

        _productRepository.Update(product);

        var saved = await _productRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure(SERVER_SIDE_ERROR);
    }
}
