using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Products;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;
using EShop.Application.Interfaces.Services;

namespace EShop.Infrastructure.Handlers.Commands.Products.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateProductCommand"/>
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid, Error>>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;

    public CreateProductCommandHandler(IProductRepository productRepository, IProductService productService)
    {
        _productRepository = productRepository;
        _productService = productService;
    }


    public async Task<Result<Guid, Error>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (!await _productService.IsNameUniqueAsync(request.Name, cancellationToken))
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

