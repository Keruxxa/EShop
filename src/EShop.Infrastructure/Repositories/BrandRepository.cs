using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IEShopDbContext _dbContext;

    public BrandRepository(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Brand>> GetList(CancellationToken cancellationToken)
    {
        return await _dbContext.Brands
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Brand> GetById(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Brands
            .Include(brand => brand.BrandProducts)
            .FirstOrDefaultAsync(brand => brand.Id == id, cancellationToken);
    }

    public int Create(Brand brand)
    {
        return _dbContext.Brands.Add(brand).Entity.Id;
    }

    public void Update(Brand brand)
    {
        _dbContext.Brands.Update(brand);
    }

    public void Delete(Brand brand)
    {
        _dbContext.Brands.Remove(brand);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
