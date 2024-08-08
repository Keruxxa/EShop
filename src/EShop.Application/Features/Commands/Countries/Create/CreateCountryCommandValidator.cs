using FluentValidation;

namespace EShop.Application.Features.Commands.Countries.Create;

/// <summary>
///     Представляет валидатор команды <see cref="CreateCountryCommand"/>
/// </summary>
public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
        => RuleFor(command => command.Name)
               .NotEmpty()
               .MaximumLength(128);
}
