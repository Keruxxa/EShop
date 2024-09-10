using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly IEShopDbContext _dbContext;

    public CountryRepository(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Country>> GetList(CancellationToken cancellationToken)
    {
        return await _dbContext.Countries
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Country> GetById(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Countries
            .FirstOrDefaultAsync(country => country.Id == id, cancellationToken);
    }

    public int Create(Country country)
    {
        return _dbContext.Countries.Add(country).Entity.Id;
    }

    public void Update(Country country)
    {
        _dbContext.Countries.Update(country);
    }

    public void Delete(Country country)
    {
        _dbContext.Countries.Remove(country);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
