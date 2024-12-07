using CSharpFunctionalExtensions;
using EShop.Application.Dtos.User;
using EShop.Application.Issues.Errors.Base;
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
    string Password) : IRequest<Result<SignUpUserResponseDto, Error>>
{
    public string HashPassword { get; private set; }

    public void SetHashPassword(string hashPassword)
        => HashPassword = hashPassword;
}
