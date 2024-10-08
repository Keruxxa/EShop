namespace EShop.Application.Interfaces.Services;

public interface IProductService
{
    /// <summary>
    ///     Проверяет, является ли <paramref name="name"/> уникальным
    /// </summary>
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверяет, существует ли товар с данным Id
    /// </summary>
    Task<bool> IsProductExistAsync(Guid id, CancellationToken cancellationToken);
}
