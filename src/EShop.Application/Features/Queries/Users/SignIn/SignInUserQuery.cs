using CSharpFunctionalExtensions;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Users.SignIn;

/// <summary>
///     Представляет запрос для входа пользователя в систему
/// </summary>
public record SignInUserQuery(string Email, string Password) : IRequest<Result<User>>
{
    public string HashPassword { get; set; }
}
