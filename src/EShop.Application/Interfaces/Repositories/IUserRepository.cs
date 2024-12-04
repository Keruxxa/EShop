using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface IUserRepository
{
    /// <summary>
    ///     Получает список пользователей
    /// </summary>
    IQueryable<User> GetListAsync();

    /// <summary>
    ///     Получает пользователя
    /// </summary>
    Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получает пользователя по email
    /// </summary>
    Task<User> SignInAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет пользователя
    /// </summary>
    Guid Add(User user);

    /// <summary>
    ///     Обновляет пользователя
    /// </summary>
    void Update(User user);

    /// <summary>
    ///     Удаляет пользователя
    /// </summary>
    void Delete(User user);

    /// <summary>
    ///     Сохраняет изменения контекста
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
