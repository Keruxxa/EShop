using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
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
    string Password) : IRequest<Result<Guid, Error>>
{
    public string HashPassword { get; private set; }

    public void SetHashPassword(string hashPassword)
        => HashPassword = hashPassword;
}
