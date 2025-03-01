namespace EShop.Application.Dtos.Review;

/// <summary>
///     Представляет объект DTO списка отзывов
/// </summary>
/// <param name="ProductId"> Id товара </param>
/// <param name="UserId"> Id пользователя </param>
/// <param name="Rating"> Рейтинг </param>
/// <param name="Text"> Текстовая часть </param>
/// <param name="UserFirstName"> Имя пользователя </param>
/// <param name="UserLastName"> Фамилия пользователя </param>
public record ReviewListItemByProductIdDto(
    Guid ProductId,
    Guid UserId,
    int Rating,
    string? Text,
    string? UserFirstName,
    string? UserLastName);
