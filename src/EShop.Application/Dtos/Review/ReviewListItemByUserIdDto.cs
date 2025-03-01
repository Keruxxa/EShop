namespace EShop.Application.Dtos.Review;

/// <summary>
///     Представляет объект DTO списка отзывов по Id пользователя
/// </summary>
/// <param name="ProductId"> Id товара </param>
/// <param name="UserId"> Id пользователя </param>
/// <param name="ProductName"> Наименование товара </param>
/// <param name="Rating"> Рейтинг </param>
/// <param name="Text"> Текстовая часть </param>
public record ReviewListItemByUserIdDto(
    Guid ProductId,
    Guid UserId,
    string ProductName,
    int Rating,
    string? Text);