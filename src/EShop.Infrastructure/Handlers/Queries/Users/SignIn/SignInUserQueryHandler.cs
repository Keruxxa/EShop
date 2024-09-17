using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Security;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Users.SignIn;

/// <summary>
///     Представляет обработчик запроса <see cref="SignInUserQuery"/>
/// </summary>
public class SignInUserCommandHandler : IRequestHandler<SignInUserQuery, Result<User>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public SignInUserCommandHandler(IEShopDbContext dbContext, IPasswordHasher passwordHasher, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }


    public async Task<Result<User>> Handle(SignInUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SignInAsync(request.Email, cancellationToken);

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