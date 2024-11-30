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
            .Include(review => review.User)
            .Where(review => review.ProductId == productId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Review>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews
            .Include(review => review.Product)
            .Where(review => review.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Review> GetByIdAsync(Guid productId, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews.FirstOrDefaultAsync(review =>
            review.ProductId == productId && review.UserId == userId, cancellationToken);
    }

    public Review Create(Review review)
    {
        return _dbContext.Reviews.Add(review).Entity;
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
