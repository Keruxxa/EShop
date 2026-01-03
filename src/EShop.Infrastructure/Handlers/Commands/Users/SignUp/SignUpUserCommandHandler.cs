using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.Dtos.User;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using EShop.Infrastructure.Utilities;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Options;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Users.SignUp;

/// <summary>
///     Представляет обработчик команды <see cref="SignUpUserCommandHandler"/>
/// </summary>
public class SignUpUserCommandHandler(
        IEShopDbContext dbContext,
        IPasswordHasher passwordHasher,
        IUserService userService,
        IMapper mapper,
        IJwtTokenService jwtTokenService,
        IOptions<JwtOptions> options) : IRequestHandler<SignUpUserCommand, Result<SignUpUserResponseDto, Error>>
{
    private readonly JwtOptions options = options.Value;

    public async Task<Result<SignUpUserResponseDto, Error>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        if (!await userService.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            return Result.Failure<SignUpUserResponseDto, Error>(new Error(
                new DuplicateEntityError(nameof(User), USER_EMAIL_IS_NOT_UNIQUE), ErrorType.Duplicate));
        }

        if (request.Phone is not null)
        {
            if (!await userService.IsPhoneUniqueAsync(request.Phone, cancellationToken))
            {
                return Result.Failure<SignUpUserResponseDto, Error>(new Error(
                    new DuplicateEntityError(nameof(User), USER_PHONE_IS_NOT_UNIQUE), ErrorType.Duplicate));
            }
        }

        request.SetHashPassword(passwordHasher.Hash(request.Password));

        var user = mapper.From(request).AdaptToType<User>();

        dbContext.Users.Add(user);

        var accessToken = jwtTokenService.GenerateAccessToken(user.Id, user.RoleId);

        var refreshToken = jwtTokenService.GenerateRefreshToken(user.Id);

        dbContext.RefreshTokens.Add(refreshToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<SignUpUserResponseDto, Error>(new SignUpUserResponseDto(user.Id, accessToken, refreshToken.Token));
    }
}
