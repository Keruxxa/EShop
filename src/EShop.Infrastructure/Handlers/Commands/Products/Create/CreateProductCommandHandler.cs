using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Products;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

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
            request.BrandId,
            request.CategoryId,
            request.Price,
            request.Description,
            request.ReleaseDate,
            request.CountryManufacturerId)
        {
            Id = Guid.NewGuid()
        };

        var images = request.ImagesInfo.Select(image => new ProductImage(product.Id, image.FileName, image.IsMain, image.Order));

        product.AddImages(images);

        _productRepository.Add(product);

        var isSaved = await _productRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Guid, Error>(product.Id)
            : Result.Failure<Guid, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}

