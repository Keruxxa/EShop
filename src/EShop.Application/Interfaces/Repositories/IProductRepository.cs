using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface IProductRepository
{
    /// <summary>
    ///     Получает список товаров
    /// </summary>
    Task<List<Product>> GetList(CancellationToken cancellationToken);

    /// <summary>
    ///     Получает товар с заполненными навигационными свойствами
    /// </summary>
    Task<Product> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получает товар с пустыми навигационными свойствами
    /// </summary>
    Task<Product> GetByIdEmptyNavProps(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создает товар
    /// </summary>
    Guid Create(Product product);

    /// <summary>
    ///     Обновляет товар
    /// </summary>
    void Update(Product product);

    /// <summary>
    ///     Удаляет товар
    /// </summary>
    void Delete(Product product);

    /// <summary>
    ///     Сохраняет изменения контекста
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
