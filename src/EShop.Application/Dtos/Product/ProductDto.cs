using EShop.Application.Dtos.ProductImages;

namespace EShop.Application.Dtos.Product;

/// <summary>
///     Представляет объект DTO товара
/// </summary>
/// <param name="Name"> Наименование </param>
/// <param name="Description"> Описание </param>
/// <param name="ReleaseDate"> Дата выпуска </param>
/// <param name="Price"> Цена </param>
/// <param name="CategoryName"> Наименование категории </param>
/// <param name="CountryManufacturerName"> Наименование страны-производителя </param>
public record ProductDto(
    Guid Id,
    string Name,
    decimal Price,
    string CategoryName,
    IEnumerable<ProductImageDto> Images,
    string? CountryManufacturerName,
    string? Description,
    DateTime? ReleaseDate);
