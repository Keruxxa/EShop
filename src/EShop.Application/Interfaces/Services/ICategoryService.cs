namespace EShop.Application.Interfaces.Services;

public interface ICategoryService
{
    /// <summary>
    ///     Проверяет, является ли <paramref name="name"/> уникальным
    /// </summary>
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверяет, существует ли категория с указанным id
    /// </summary>
    Task<bool> IsCategoryExistAsync(int id, CancellationToken cancellationToken);
}
