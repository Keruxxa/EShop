namespace EShop.Application.Dtos.Review;

/// <summary>
///     Представляет объект DTO для обновления отзыва
/// </summary>
/// <param name="Id"> Id отзыва </param>
/// <param name="Rating"> Рейтинг </param>
/// <param name="Text"> Текстовая часть отзыва </param>
public record UpdateReviewDto(Guid Id, int Rating, string? Text);
