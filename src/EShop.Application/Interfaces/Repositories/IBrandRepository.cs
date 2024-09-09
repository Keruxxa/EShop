using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface IBrandRepository
{
    /// <summary>
    ///     Получает список брендов
    /// </summary>
    Task<List<Brand>> GetList(CancellationToken cancellationToken);

    /// <summary>
    ///     Получает бренд
    /// </summary>
    Task<Brand> GetById(int id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создает бренд
    /// </summary>
    int Create(Brand brand);

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
