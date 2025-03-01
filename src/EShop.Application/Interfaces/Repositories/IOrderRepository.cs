using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    /// <summary>
    ///     Получает список заказов пользователя
    /// </summary>
    Task<List<Order>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Получает заказ
    /// </summary>
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет заказ
    /// </summary>
    Guid Add(Order order);

    /// <summary>
    ///     Сохраняет изменения контекста
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
