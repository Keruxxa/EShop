using EShop.Application.Models;
using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    /// <summary>
    ///     Получает список категорий
    /// </summary>
    IQueryable<Category> GetList();

    /// <summary>
    ///     Получает категорию
    /// </summary>
    Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получает список родительских подкатегорий
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    Task<List<SelectListItem<int>>> GetHierarchyByIdAsync(int categoryId, CancellationToken cancellationToken);

    /// <summary>
    ///     Создает категорию
    /// </summary>
    Task<bool> CreateAsync(Category category, List<int> ancestorIds, CancellationToken cancellationToken);

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
