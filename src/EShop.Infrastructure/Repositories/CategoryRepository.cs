using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

class CategoryRepository : ICategoryRepository
{
    private readonly IEShopDbContext _dbContext;

    public CategoryRepository(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Category>> GetList(CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Category> GetById(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
    }

    public int Create(Category category)
    {
        return _dbContext.Categories.Add(category).Entity.Id;
    }

    public void Update(Category category)
    {
        _dbContext.Categories.Update(category);
    }

    public void Delete(Category category)
    {
        _dbContext.Categories.Remove(category);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
