using EShop.Application.Interfaces.Services;
using EShop.Infrastructure.Data;

namespace EShop.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly EShopDbContext _dbContext;

    public OrderService(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
