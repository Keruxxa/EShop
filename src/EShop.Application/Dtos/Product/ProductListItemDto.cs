using EShop.Application.Dtos.ProductImages;

namespace EShop.Application.Dtos.Product;

/// <summary>
///     Представляет объект DTO товара для отображения в списке
/// </summary>
/// <param name="Name"> Наименование </param>
/// <param name="Description"> Описание </param>
/// <param name="ReleaseDate"> Дата выпуска </param>
/// <param name="Price"> Цена </param>
/// <param name="RatingCount"> Количество отзывов </param>
/// <param name="Rating"> Рейтинг </param>
public record ProductListItemDto(
    Guid Id,
    string Name,
    decimal Price,
    int ReviewCount,
    IEnumerable<ProductImageDto> Images,
    string? Description,
    DateTime? ReleaseDate,
    decimal? Rating);
