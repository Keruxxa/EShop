using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Auth;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.RefreshTokens;

/// <summary>
///     Представляет комманду для обновления токена
/// </summary>
/// <param name="RefreshToken"></param>
public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<RefreshTokenResponse, Error>>;
