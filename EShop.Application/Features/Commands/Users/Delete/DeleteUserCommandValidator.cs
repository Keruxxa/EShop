using FluentValidation;

namespace EShop.Application.Features.Commands.Users.Delete;

/// <summary>
///     Преставялет валидатор команды <see cref="DeleteUserCommand"/>
/// </summary>
public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
        => RuleFor(command => command.Id).NotEmpty();
}
