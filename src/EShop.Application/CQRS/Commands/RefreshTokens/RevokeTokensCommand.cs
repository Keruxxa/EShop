using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.RefreshTokens;

/// <summary>
///     Представляет команду для отзыва всех токенов
/// </summary>
public record RevokeTokensCommand(Guid UserId) : IRequest<Result<Unit, Error>>;
