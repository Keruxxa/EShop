using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface ICountryRepository
{
    /// <summary>
    ///     Получает список категорий
    /// </summary>
    Task<List<Country>> GetList(CancellationToken cancellationToken);

    /// <summary>
    ///     Получает страну
    /// </summary>
    Task<Country> GetById(int id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создает страну
    /// </summary>
    int Create(Country country);

    /// <summary>
    ///     Обновляет страну
    /// </summary>
    void Update(Country country);

    /// <summary>
    ///     Удаляет страну
    /// </summary>
    void Delete(Country country);

    /// <summary>
    ///     Сохраняет изменения контекста
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
