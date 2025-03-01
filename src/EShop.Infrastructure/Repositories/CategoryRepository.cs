using CSharpFunctionalExtensions;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Models;
using EShop.Domain.Entities;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

class CategoryRepository : ICategoryRepository
{
    private readonly EShopDbContext _dbContext;

    public CategoryRepository(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public IQueryable<Category> GetList()
    {
        return _dbContext.Categories
            .AsNoTracking()
            .AsQueryable();
    }

    public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
    }

    public async Task<List<SelectListItem<int>>> GetHierarchyByIdAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.CategoryClosureNodes
            .Include(categoryClosureNode => categoryClosureNode.AncestorCategory)
            .Where(categoryClosureNode => categoryClosureNode.DescendantCategoryId == categoryId)
            .OrderBy(categoryClosureNode => categoryClosureNode.AncestorCategoryId)
            .Select(categoryClosureNode => SelectListItem<int>.CreateItem(categoryClosureNode.AncestorCategory!))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AddAsync(Category category, List<int> ancestorIds, CancellationToken cancellationToken)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            _dbContext.Categories.Add(category);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var categoryClosureNodes = ancestorIds
                .Select(ancestorId => new CategoryClosureNode(ancestorId, category.Id))
                .Append(new CategoryClosureNode(category.Id, category.Id));

            _dbContext.CategoryClosureNodes.AddRange(categoryClosureNodes);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return true;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return false;
        }
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
