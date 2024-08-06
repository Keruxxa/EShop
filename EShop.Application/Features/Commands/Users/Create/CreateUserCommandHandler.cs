using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using Mapster;
using MediatR;

using static EShop.Application.Constants;

namespace EShop.Application.Features.Commands.Users.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateUserCommandHandler"/>
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserService _userService;

    public CreateUserCommandHandler(
        IEShopDbContext dbContext,
        IPasswordHasher passwordHasher,
        IUserService userService)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _userService = userService;
    }


    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userService.IsEmailUnique(request.Email, cancellationToken))
        {
            throw new DuplicateEntityException(nameof(User));
        }

        if (await _userService.IsPhoneUnique(request.Phone, cancellationToken))
        {
            throw new DuplicateEntityException(nameof(User));
        }

        request.HashPassword = _passwordHasher.Hash(request.Password);

        var user = request.Adapt<User>();

        _dbContext.Users.Add(user);

        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success(user.Id)
            : Result.Failure<Guid>(SERVER_SIDE_ERROR);
    }
}
