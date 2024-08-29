using EShop.Application.CQRS.Commands.Users;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Users.Delete;

/// <summary>
///     Преставялет валидатор команды <see cref="DeleteUserCommand"/>
/// </summary>
public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator() =>
        RuleFor(command => command.Id).NotEmpty();
}
