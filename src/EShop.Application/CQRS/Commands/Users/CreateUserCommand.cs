using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.CQRS.Commands.Users;

/// <summary>
///     Представляет команду для создания пользователя
/// </summary>
public record CreateUserCommand(
    string? FirstName,
    string? LastName,
    string? Phone,
    string Email,
    string Password) : IRequest<Result<Guid>>
{
    public string HashPassword { get; private set; }

    public void SetHashPassword(string hashPassword)
        => HashPassword = hashPassword;
}
