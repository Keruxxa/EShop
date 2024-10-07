namespace EShop.Application.Interfaces.Services;

public interface IBrandService
{
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);
}
