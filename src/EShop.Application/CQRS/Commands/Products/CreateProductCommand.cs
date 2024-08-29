using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.CQRS.Commands.Products;

/// <summary>
///     Представляет команду для создания товара
/// </summary>
public record CreateProductCommand(
    string Name,
    string? Description,
    DateTime? ReleaseDate,
    decimal Price,
    int CategoryId,
    int BrandId,
    int? CountryManufacturerId) : IRequest<Result<Guid>>;
