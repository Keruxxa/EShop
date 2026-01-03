using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.RefreshTokens;

/// <summary>
///     Представляет команду для отзыва токена
/// </summary>
public record RevokeTokenCommand(string Token) : IRequest<Result<Unit, Error>>;
