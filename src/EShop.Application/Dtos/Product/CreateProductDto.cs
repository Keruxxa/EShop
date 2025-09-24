using Microsoft.AspNetCore.Http;

namespace EShop.Application.Dtos.Product;

/// <summary>
///     Представляет объект DTO для создания товара
/// </summary>
/// <param name="Name"> Наименование </param>
/// <param name="Description"> Описание </param>
/// <param name="ReleaseDate"> Дата выпуска </param>
/// <param name="Price"> Цена </param>
/// <param name="Images"> Изображения в виде файлов </param>
/// <param name="ImagesInfo"> JSON-строка информации по изображениям </param>
/// <param name="CategoryId"> Id категории </param>
/// <param name="BrandId"> Id бренда </param>
/// <param name="CountryManufacturerId"> Id страны-производителя </param>
public record CreateProductDto(
    string Name,
    int CategoryId,
    int BrandId,
    decimal Price,
    IEnumerable<IFormFile> Images,
    string ImagesInfo,
    string? Description,
    DateTime? ReleaseDate,
    int? CountryManufacturerId);
