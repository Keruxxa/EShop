namespace EShop.Application.Interfaces.Services;

public interface IUserService
{
    Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken);

    Task<bool> IsPhoneUnique(string phone, CancellationToken cancellationTokenn);
}
