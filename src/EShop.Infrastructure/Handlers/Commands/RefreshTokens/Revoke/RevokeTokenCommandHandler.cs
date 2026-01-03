using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.RefreshTokens;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using EShop.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Commands.RefreshTokens.Revoke;

/// <summary>
///     Представялет обработчик команды <see cref="RevokeTokenCommand"/>
/// </summary>
public class RevokeTokenCommandHandler(EShopDbContext dbContext) : IRequestHandler<RevokeTokenCommand, Result<Unit, Error>>
{
    public async Task<Result<Unit, Error>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var isUpdated = await dbContext.RefreshTokens
            .Where(rt => rt.Token == request.Token)
            .ExecuteUpdateAsync(s => s.SetProperty(rt => rt.IsRevoked, true), cancellationToken);

        if (isUpdated == 0)
        {
            return Result.Failure<Unit, Error>(new Error(
                new NotFoundEntityError(nameof(RefreshToken), request.Token), ErrorType.NotFound));
        }

        return Result.Success<Unit, Error>(Unit.Value);
    }
}
