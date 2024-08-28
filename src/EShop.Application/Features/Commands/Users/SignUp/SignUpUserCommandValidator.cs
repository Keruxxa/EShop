using FluentValidation;

namespace EShop.Application.Features.Commands.Users.SignUp;

/// <summary>
///     Преставялет валидатор команды <see cref="SignUpUserCommand"/>
/// </summary>
public class SignUpUserCommandValidator : AbstractValidator<SignUpUserCommand>
{
    public SignUpUserCommandValidator()
    {
        RuleFor(command => command.FirstName).MaximumLength(32);

        RuleFor(command => command.LastName).MaximumLength(32);

        RuleFor(command => command.Email).MaximumLength(64);

        RuleFor(command => command.Phone).MaximumLength(11);

        RuleFor(command => command.Password).MaximumLength(64);
    }
}
