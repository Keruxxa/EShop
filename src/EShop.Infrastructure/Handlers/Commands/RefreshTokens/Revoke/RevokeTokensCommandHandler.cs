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
///     Представялет обработчик команды <see cref="RevokeTokensCommand"/>
/// </summary>
public class RevokeTokensCommandHandler(EShopDbContext dbContext) : IRequestHandler<RevokeTokensCommand, Result<Unit, Error>>
{
    public async Task<Result<Unit, Error>> Handle(RevokeTokensCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await dbContext.RefreshTokens
            .Where(rt => rt.UserId == request.UserId)
            .ExecuteUpdateAsync(s => s.SetProperty(rt => rt.IsRevoked, true), cancellationToken);

        if (isDeleted == 0)
        {
            return Result.Failure<Unit, Error>(new Error(
                new NotFoundEntityError(nameof(RefreshToken), request.UserId), ErrorType.NotFound));
        }

        return Result.Success<Unit, Error>(Unit.Value);
    }
}
