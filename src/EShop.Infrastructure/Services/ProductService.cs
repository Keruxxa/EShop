using EShop.Application.Interfaces.Services;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly EShopDbContext _dbContext;

    public ProductService(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return !await _dbContext.Products.AnyAsync(product => product.Name.Equals(name), cancellationToken);
    }
}
