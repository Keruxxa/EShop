using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly EShopDbContext _dbContext;

    public ReviewRepository(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Review>> GetListByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews
            .Where(review => review.ProductId == productId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Review>> GetListByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews
            .Where(review => review.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public Guid Create(Review review)
    {
        return _dbContext.Reviews.Add(review).Entity.Id;
    }

    public void Update(Review review)
    {
        _dbContext.Reviews.Update(review);
    }

    public void Delete(Review review)
    {
        _dbContext.Reviews.Remove(review);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
