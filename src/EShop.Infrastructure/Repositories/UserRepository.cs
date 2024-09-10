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


    public async Task<List<User>> GetList(CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Where(user => user.RoleId == RoleType.Manager)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<User> SignIn(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Email.Equals(email), cancellationToken);
    }

    public Guid Create(User user)
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
