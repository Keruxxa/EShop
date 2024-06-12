namespace EShop.Infrastructure.Dtos.Review
{
    /// <param name="ProductId"> Id товара, связанного с отзывом </param>
    /// <param name="Rating"> Рейтинг </param>
    /// <param name="Text"> Текстовая часть отзыва </param>
    public record CreateReviewDto(Guid ProductId, int Rating, string? Text);
}
