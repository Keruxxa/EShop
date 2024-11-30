namespace EShop.Application.Dtos.Review;

/// <summary>
///     Представляет объект DTO для обновления отзыва
/// </summary>
/// <param name="ProductId"> Id товара </param>
/// <param name="UserId"> Id пользователя </param>
/// <param name="Rating"> Рейтинг </param>
/// <param name="Rating"> Рейтинг </param>
/// <param name="Text"> Текстовая часть отзыва </param>
public record UpdateReviewDto(Guid ProductId, Guid UserId, int Rating, string? Text);