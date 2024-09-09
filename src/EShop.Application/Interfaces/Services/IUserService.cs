namespace EShop.Application.Interfaces.Services;

public interface IUserService
{
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);

    Task<bool> IsPhoneUniqueAsync(string phone, CancellationToken cancellationTokenn);
}
