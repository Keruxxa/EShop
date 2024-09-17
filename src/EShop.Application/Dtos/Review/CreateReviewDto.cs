namespace EShop.Application.Dtos.Review;

/// <summary>
///     Представляет объект DTO для создания отзыва
/// </summary>
/// <param name="UserId"> Id пользователя </param>
/// <param name="ProductId"> Id товара </param>
/// <param name="Rating"> Рейтинг </param>
/// <param name="Text"> Текстовая часть отзыва </param>
public record CreateReviewDto(Guid UserId, Guid ProductId, int Rating, string? Text);
