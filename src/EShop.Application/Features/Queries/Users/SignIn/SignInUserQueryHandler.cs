using CSharpFunctionalExtensions;
using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

/// <summary>
///     Представляет обработчик запроса <see cref="SignInUserQuery"/>
/// </summary>
namespace EShop.Application.Features.Queries.Users.SignIn;

public class SignInUserCommandHandler : IRequestHandler<SignInUserQuery, Result<User>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public SignInUserCommandHandler(IEShopDbContext dbContext, IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }


    public async Task<Result<User>> Handle(SignInUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Email.Equals(request.Email), cancellationToken);

        if (user is null)
        {
            return Result.Failure<User>("User with this email and password doesn't exist");
        }

        if (!_passwordHasher.Verify(request.Password, user.Password))
        {
            return Result.Failure<User>("Password is wrong");
        }

        return Result.Success(user);
    }
}
