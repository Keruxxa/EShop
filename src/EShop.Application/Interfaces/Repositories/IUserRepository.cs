using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface IUserRepository
{
    /// <summary>
    ///     Получает список пользователей
    /// </summary>
    Task<List<User>> GetList(CancellationToken cancellationToken);

    /// <summary>
    ///     Получает пользователя
    /// </summary>
    Task<User> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получает пользователя по email
    /// </summary>
    Task<User> SignIn(string email, CancellationToken cancellationToken);

    /// <summary>
    ///     Создает пользователя
    /// </summary>
    Guid Create(User user);

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
