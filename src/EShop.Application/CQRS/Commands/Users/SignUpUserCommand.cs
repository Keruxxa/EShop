using CSharpFunctionalExtensions;
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
    string Password) : IRequest<Result<User>>
{
    public string HashPassword { get; set; }
}
