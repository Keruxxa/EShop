using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface IFavoriteRepository
{
    /// <summary>
    ///     Получает коллекцию избранного
    /// </summary>
    Task<Favorite> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаляет коллекцию избранного
    /// </summary>
    void Delete(Favorite favorite);

    /// <summary>
    ///     Сохраняет изменения контекста
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
