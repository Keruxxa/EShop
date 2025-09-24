using CSharpFunctionalExtensions;
using EShop.Application.Dtos.ProductImages;
using EShop.Application.Issues.Errors.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.CQRS.Commands.Products;

/// <summary>
///     Представляет команду для создания товара
/// </summary>
public record CreateProductCommand(
    string Name,
    int CategoryId,
    int BrandId,
    decimal Price,
    IEnumerable<IFormFile> Images,
    IEnumerable<CreateProductImageInfo> ImagesInfo,
    string? Description,
    DateTime? ReleaseDate,
    int? CountryManufacturerId) : IRequest<Result<Guid, Error>>;
