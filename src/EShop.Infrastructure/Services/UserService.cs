using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IEShopDbContext _dbContext;

    public UserService(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken)
    {
        return !await _dbContext.Users
            .AnyAsync(user => user.Email.Equals(email), cancellationToken);
    }

    public async Task<bool> IsPhoneUniqueAsync(string phone, CancellationToken cancellationToken)
    {
        return !await _dbContext.Users
            .AnyAsync(user => user.Phone.Equals(phone), cancellationToken);
    }

    public async Task<bool> IsUserExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(user => user.Id == id, cancellationToken);
    }
}
