using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IEShopDbContext _dbContext;

    public UserRepository(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public IQueryable<User> GetListAsync()
    {
        return _dbContext.Users.Where(user => user.RoleId == RoleType.Manager);
    }

    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<User> SignInAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Email.Equals(email), cancellationToken);
    }

    public Guid Add(User user)
    {
        return _dbContext.Users.Add(user).Entity.Id;
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
