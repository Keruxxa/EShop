using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface IReviewRepository
{
    /// <summary>
    ///     Получает список отзывов товара
    /// </summary>
    IQueryable<Review> GetListByProductId(Guid productId);

    /// <summary>
    ///     Получает список отзывов пользователя
    /// </summary>
    IQueryable<Review> GetListByUserId(Guid userId);

    /// <summary>
    ///     Получает отзыв
    /// </summary>
    Task<Review?> GetByIdAsync(Guid productId, Guid userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет отзыв
    /// </summary>
    Review Add(Review review);

    /// <summary>
    ///     Обновляет отзыва
    /// </summary>
    void Update(Review review);

    /// <summary>
    ///     Удаляет отзыв
    /// </summary>
    void Delete(Review review);

    /// <summary>
    ///     Сохряняет изменения контекста
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
