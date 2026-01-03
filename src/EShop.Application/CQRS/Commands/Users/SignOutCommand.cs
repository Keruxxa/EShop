using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Users;

/// <summary>
///     Представляется команду для выхода пользователя из системы
/// </summary>
/// <param name="RefreshToken"></param>
public record SignOutCommand(string RefreshToken) : IRequest<Result<Unit, Error>>;
