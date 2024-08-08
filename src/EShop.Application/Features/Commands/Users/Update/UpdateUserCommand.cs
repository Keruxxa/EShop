using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.Features.Commands.Users.Update;

/// <summary>
///     Представляет команду для обновления пользователя
/// </summary>
public record UpdateUserCommand(
    Guid Id,
    string? FirstName,
    string? LastName) : IRequest<Result<bool>>;
