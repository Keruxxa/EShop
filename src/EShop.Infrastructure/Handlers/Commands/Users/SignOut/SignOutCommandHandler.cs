using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.Issues.Errors.Base;
using EShop.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Commands.Users.SignOut;

/// <summary>
///     Представляет обработчик команды <see cref="SignOutCommand"/>
/// </summary>
public class SignOutCommandHandler(EShopDbContext dbContext) : IRequestHandler<SignOutCommand, Result<Unit, Error>>
{
    public async Task<Result<Unit, Error>> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await dbContext.RefreshTokens
            .Where(rt => rt.Token == request.RefreshToken)
            .ExecuteUpdateAsync(s => s.SetProperty(rt => rt.IsRevoked, true), cancellationToken);

        return Result.Success<Unit, Error>(Unit.Value);
    }
}
