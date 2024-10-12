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

    public async Task<bool> IsProductExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Products.AnyAsync(product => product.Id == id, cancellationToken);
    }

    public async Task<bool> IsAllProductsExistAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken)
    {
        if (!productIds.Any())
        {
            return false;
        }

        var exist = false;

        await _dbContext.Products.ForEachAsync(product =>
        {
            exist = productIds.Contains(product.Id);
        });

        return exist;
    }
}
