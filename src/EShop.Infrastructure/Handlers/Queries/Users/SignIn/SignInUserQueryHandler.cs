using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Dtos.User;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Security;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using EShop.Infrastructure.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Queries.Users.SignIn;

/// <summary>
///     Представляет обработчик запроса <see cref="SignInUserQuery"/>
/// </summary>
public class SignInUserCommandHandler(
        IEShopDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IOptions<JwtOptions> options) : IRequestHandler<SignInUserQuery, Result<SignInUserResponseDto, Error>>
{
    private readonly JwtOptions _options = options.Value;

    public async Task<Result<SignInUserResponseDto, Error>> Handle(SignInUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(u => u.Email == request.Email)
            .Select(u => new
            {
                u.Id,
                u.RoleId,
                u.Password
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<SignInUserResponseDto, Error>(new Error(
                new NotFoundEntityError(nameof(User), request.Email), ErrorType.BadRequest));
        }

        if (!passwordHasher.Verify(request.Password, user.Password))
        {
            return Result.Failure<SignInUserResponseDto, Error>(new Error(
                new BadRequestEntityError(USER_WRONG_PASSWORD), ErrorType.BadRequest));
        }

        var accessToken = jwtTokenService.GenerateAccessToken(user.Id, user.RoleId);

        var refreshToken = jwtTokenService.GenerateRefreshToken(user.Id);

        dbContext.RefreshTokens.Add(refreshToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<SignInUserResponseDto, Error>(new SignInUserResponseDto(user.Id, accessToken, refreshToken.Token));
    }
}
