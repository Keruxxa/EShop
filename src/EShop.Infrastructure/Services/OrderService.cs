using EShop.Application.Interfaces.Services;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly EShopDbContext _dbContext;

    public OrderService(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
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
