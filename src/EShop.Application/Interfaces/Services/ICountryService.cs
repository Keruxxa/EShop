namespace EShop.Application.Interfaces.Services;

public interface ICountryService
{
    /// <summary>
    ///     Проверяет, является ли <paramref name="name"/> уникальным
    /// </summary>
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);
}
