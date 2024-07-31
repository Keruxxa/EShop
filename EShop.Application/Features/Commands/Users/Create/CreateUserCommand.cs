using CSharpFunctionalExtensions;
using EShop.Domain.Enums;
using MediatR;

namespace EShop.Application.Features.Commands.Users.Create
{
    /// <summary>
    ///     Представляет команду для создания пользователя
    /// </summary>
    public record CreateUserCommand(
        string? FirstName,
        string? LastName,
        string? Phone,
        string Email,
        string Password,
        RoleType RoleId = RoleType.UnregisteredUser) : IRequest<Result<Guid>>
    {
        public string HashPassword { get; set; }
    }
}
