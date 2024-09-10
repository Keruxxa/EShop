using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    /// <summary>
    ///     Получает список категорий
    /// </summary>
    Task<List<Category>> GetList(CancellationToken cancellationToken);

    /// <summary>
    ///     Получает категорию
    /// </summary>
    Task<Category> GetById(int id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создает категорию
    /// </summary>
    int Create(Category category);

    /// <summary>
    ///     Обновляет категорию
    /// </summary>
    void Update(Category category);

    /// <summary>
    ///     Удаляет категорию
    /// </summary>
    void Delete(Category category);

    /// <summary>
    ///     Сохраняет изменения контекста
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
