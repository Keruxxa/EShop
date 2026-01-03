using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.RefreshTokens;
using EShop.Application.Dtos.Auth;
using EShop.Application.Interfaces.Security;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Commands.RefreshTokens.Refresh;

/// <summary>
///     Представялет обработчик команды <see cref="RefreshTokenCommand"/>
/// </summary>
public class RefreshTokenCommandHandler(EShopDbContext dbContext, IJwtTokenService jwtTokenService) :
    IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse, Error>>
{
    public async Task<Result<RefreshTokenResponse, Error>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

        if (refreshToken is null || refreshToken.ExpireDate <= DateTime.UtcNow || refreshToken.IsRevoked)
        {
            return Result.Failure<RefreshTokenResponse, Error>(new Error(
                new UnauthorizedError(), ErrorType.Unauthorized));
        }

        refreshToken.IsRevoked = true;

        var newRefreshToken = jwtTokenService.GenerateRefreshToken(refreshToken.UserId);

        dbContext.RefreshTokens.Add(newRefreshToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        var accessToken = await GetAccessToken(refreshToken.UserId, cancellationToken);

        return Result.Success<RefreshTokenResponse, Error>(new RefreshTokenResponse(accessToken, newRefreshToken.Token));
    }

    private async Task<string> GetAccessToken(Guid userId, CancellationToken cancellationToken)
    {
        var userRoleType = await dbContext.Users
            .Where(u => u.Id == userId)
            .Select(u => u.RoleId)
            .FirstOrDefaultAsync(cancellationToken);

        return jwtTokenService.GenerateAccessToken(userId, userRoleType);
    }
}
