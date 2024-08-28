using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using Mapster;
using MediatR;

using static EShop.Application.Constants;

namespace EShop.Application.Features.Commands.Users.SignUp;

/// <summary>
///     Представляет обработчик команды <see cref="SignUpUserCommandHandler"/>
/// </summary>
public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, Result<User>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserService _userService;

    public SignUpUserCommandHandler(
        IEShopDbContext dbContext,
        IPasswordHasher passwordHasher,
        IUserService userService)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _userService = userService;
    }


    public async Task<Result<User>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userService.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            throw new DuplicateEntityException(nameof(User));
        }

        if (!await _userService.IsPhoneUniqueAsync(request.Phone, cancellationToken))
        {
            throw new DuplicateEntityException(nameof(User));
        }

        request.HashPassword = _passwordHasher.Hash(request.Password);

        var user = request.Adapt<User>();

        _dbContext.Users.Add(user);

        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success(user)
            : Result.Failure<User>(SERVER_SIDE_ERROR);
    }
}
