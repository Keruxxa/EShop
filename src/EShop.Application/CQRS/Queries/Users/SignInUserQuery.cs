using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.CQRS.Queries.Users;

/// <summary>
///     Представляет запрос для входа пользователя в систему
/// </summary>
public record SignInUserQuery(string Email, string Password) : IRequest<Result<User, Error>>
{
    public string HashPassword { get; set; }
}
