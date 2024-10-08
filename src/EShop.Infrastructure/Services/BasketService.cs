using EShop.Application.Interfaces.Services;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Services;

internal class BasketService : IBasketService
{
    private readonly EShopDbContext _dbContext;

    public BasketService(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> IsBasketExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Baskets.AnyAsync(basket => basket.Id == id, cancellationToken);
    }
}
