using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly EShopDbContext _dbContext;

    public OrderRepository(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Order>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.Product)
            .Where(order => order.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.Product)
            .FirstOrDefaultAsync(order => order.Id == id, cancellationToken);
    }

    public Guid Create(Order order)
    {
        return _dbContext.Orders.Add(order).Entity.Id;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
