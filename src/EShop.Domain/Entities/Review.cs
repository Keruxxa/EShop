namespace EShop.Domain.Entities;

/// <summary>
///     Представляет отзыв
/// </summary>
public class Review
{
    /// <summary>
    ///     Id пользователя
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    ///     Пользователь
    /// </summary>
    public User? User { get; }

    /// <summary>
    ///     Id товара
    /// </summary>
    public Guid ProductId { get; }

    /// <summary>
    ///     Товар
    /// </summary>
    public Product? Product { get; }

    /// <summary>
    ///     Рейтинг
    /// </summary>
    public int Rating { get; private set; }

    /// <summary>
    ///     Текстовая часть отзыва
    /// </summary>
    public string? Text { get; private set; }


    public Review(Guid productId, Guid userId, int rating, string? text)
    {
        ProductId = productId;
        UserId = userId;
        Rating = rating;
        Text = text;
    }

    /// <summary>
    ///     Обновляет поля сущности
    /// </summary>
    public void UpdateEntity(int rating, string? text)
    {
        Rating = rating;
        Text = text;
    }
}
