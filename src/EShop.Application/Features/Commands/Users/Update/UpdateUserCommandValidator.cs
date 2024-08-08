using FluentValidation;

namespace EShop.Application.Features.Commands.Users.Update;

/// <summary>
///     Преставялет валидатор команды <see cref="UpdateUserCommand"/>
/// </summary>
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.FirstName).MaximumLength(64);

        RuleFor(command => command.LastName).MaximumLength(64);
    }
}
