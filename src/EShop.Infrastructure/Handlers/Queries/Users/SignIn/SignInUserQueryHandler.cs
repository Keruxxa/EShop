using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Dtos.User;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Security;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Queries.Users.SignIn;

/// <summary>
///     Представляет обработчик запроса <see cref="SignInUserQuery"/>
/// </summary>
public class SignInUserCommandHandler : IRequestHandler<SignInUserQuery, Result<SignInUserResponseDto, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public SignInUserCommandHandler(
        IEShopDbContext dbContext,
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }


    public async Task<Result<SignInUserResponseDto, Error>> Handle(SignInUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SignInAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<SignInUserResponseDto, Error>(new Error(
                new NotFoundEntityError(nameof(User),
                request.Email), ErrorType.BadRequest));
        }

        if (!_passwordHasher.Verify(request.Password, user.Password))
        {
            return Result.Failure<SignInUserResponseDto, Error>(new Error(
                new BadRequestEntityError(USER_WRONG_PASSWORD),
                ErrorType.BadRequest));
        }

        var token = _jwtTokenService.Generate(user);

        return Result.Success<SignInUserResponseDto, Error>(new SignInUserResponseDto(user.Id, token));
    }
}