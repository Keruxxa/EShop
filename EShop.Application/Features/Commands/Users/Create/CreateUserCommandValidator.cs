using FluentValidation;

namespace EShop.Application.Features.Commands.Users.Create;

/// <summary>
///     Представляет валидатор команды <see cref="CreateProductCommand"/>
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.FirstName).MaximumLength(32);

        RuleFor(command => command.LastName).MaximumLength(32);

        RuleFor(command => command.Email)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(command => command.Phone).MaximumLength(11);

        RuleFor(command => command.Password)
            .NotEmpty()
            .MaximumLength(64);
    }
}