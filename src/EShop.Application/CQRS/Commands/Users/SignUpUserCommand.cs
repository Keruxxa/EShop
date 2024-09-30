using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.CQRS.Commands.Users;

/// <summary>
///     Представляет команду для регистрации пользователя
/// </summary>
public record SignUpUserCommand(
    string? FirstName,
    string? LastName,
    string? Phone,
    string Email,
    string Password) : IRequest<Result<User, Error>>
{
    public string HashPassword { get; private set; }

    public void SetHashPassword(string hashPassword)
        => HashPassword = hashPassword;
}
