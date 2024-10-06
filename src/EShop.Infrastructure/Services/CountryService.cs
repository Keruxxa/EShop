using EShop.Application.Interfaces.Services;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Services;

public class CountryService : ICountryService
{
    private readonly EShopDbContext _dbContext;

    public CountryService(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return !await _dbContext.Countries.AnyAsync(country => country.Name.Equals(name), cancellationToken);
    }

    public async Task<bool> IsNameUniqueAsync(string name, int id, CancellationToken cancellationToken)
    {
        return !await _dbContext.Countries.AnyAsync(country =>
            country.Name.Equals(name) && country.Id != id, cancellationToken);
    }
}
