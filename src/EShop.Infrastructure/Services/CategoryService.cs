using EShop.Application.Interfaces.Services;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly EShopDbContext _dbContext;

    public CategoryService(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return !await _dbContext.Categories
            .AnyAsync(category => category.Name.Equals(name), cancellationToken);
    }
}
