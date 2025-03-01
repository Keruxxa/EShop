using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface IBrandRepository
{
    /// <summary>
    ///     Получает список брендов
    /// </summary>
    IQueryable<Brand> GetList();

    /// <summary>
    ///     Получает бренд
    /// </summary>
    Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет бренд
    /// </summary>
    int Add(Brand brand);

    /// <summary>
    ///     Обновляет бренд
    /// </summary>
    void Update(Brand brand);

    /// <summary>
    ///     Удаляет бренд
    /// </summary>
    void Delete(Brand brand);

    /// <summary>
    ///     Сохраняет изменения контекста
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
