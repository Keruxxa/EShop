using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly EShopDbContext _dbContext;

    public FavoriteRepository(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Favorite> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Favorites
            .Include(favorite => favorite.FavoriteProducts)
                .ThenInclude(favoriteProduct => favoriteProduct.Product)
            .FirstOrDefaultAsync(favorite => favorite.Id == id, cancellationToken);
    }

    public void Create(Favorite favorite)
    {
        _dbContext.Favorites.Add(favorite);
    }

    public void Delete(Favorite favorite)
    {
        _dbContext.Favorites.Remove(favorite);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
