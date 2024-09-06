using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IEShopDbContext _dbContext;

    public ProductRepository(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Product>> GetList(CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Product> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .Include(product => product.Category)
            .Include(product => product.CountryManufacturer)
            .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
    }

    public async Task<Product> GetByIdEmptyNavProps(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);
    }

    public Guid Create(Product product)
    {
        var addedProduct = _dbContext.Products.Add(product).Entity;

        return addedProduct.Id;
    }

    public void Delete(Product product)
    {
        _dbContext.Products.Remove(product);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
