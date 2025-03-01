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


    public IQueryable<Review> GetListByProductId(Guid productId)
    {
        return _dbContext.Reviews
            .Include(review => review.User)
            .Where(review => review.ProductId == productId);
    }

    public IQueryable<Review> GetListByUserId(Guid userId)
    {
        return _dbContext.Reviews
            .Include(review => review.Product)
            .Where(review => review.UserId == userId);
    }

    public async Task<Review?> GetByIdAsync(Guid productId, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews.FirstOrDefaultAsync(review =>
            review.ProductId == productId && review.UserId == userId, cancellationToken);
    }

    public Review Add(Review review)
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
