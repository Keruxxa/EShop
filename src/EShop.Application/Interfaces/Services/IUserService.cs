namespace EShop.Application.Interfaces.Services;

public interface IUserService
{
    /// <summary>
    ///     Определяет, используется ли эл. почта другим пользователем
    /// </summary>
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    ///     Определяет, используется ли телефон другим пользователем
    /// </summary>
    Task<bool> IsPhoneUniqueAsync(string phone, CancellationToken cancellationToken);

    /// <summary>
    ///     Определяет, существует ли пользователь
    /// </summary>
    Task<bool> IsUserExistAsync(Guid id, CancellationToken cancellationToken);
}
