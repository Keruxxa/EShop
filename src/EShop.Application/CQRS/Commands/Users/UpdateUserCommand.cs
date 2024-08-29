using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.CQRS.Commands.Users;

/// <summary>
///     Представляет команду для обновления пользователя
/// </summary>
public record UpdateUserCommand(
    Guid Id,
    string? FirstName,
    string? LastName) : IRequest<Result>;
