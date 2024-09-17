using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface IReviewRepository
{
    /// <summary>
    ///     Получает список отзывов товара
    /// </summary>
    Task<List<Review>> GetListByProductIdAsync(Guid productId, CancellationToken cancellationToken);

    /// <summary>
    ///     Получает список отзывов пользователя
    /// </summary>
    Task<List<Review>> GetListByUserId(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Создает отзыв
    /// </summary>
    Guid Create(Review review);

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
