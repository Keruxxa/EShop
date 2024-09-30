using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Users;

/// <summary>
///     Представляет команду для обновления пользователя
/// </summary>
public record UpdateUserCommand(
    Guid Id,
    string? FirstName,
    string? LastName) : IRequest<Result<Unit, Error>>;
