using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly EShopDbContext _dbContext;

    public BasketRepository(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Basket> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Baskets
            .Include(basket => basket.BasketItems)
            .FirstOrDefaultAsync(basket => basket.Id == id, cancellationToken);
    }

    public void Delete(Basket basket)
    {
        _dbContext.Baskets.Remove(basket);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
