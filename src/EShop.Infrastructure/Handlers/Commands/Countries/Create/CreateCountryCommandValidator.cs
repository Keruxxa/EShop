using EShop.Application.CQRS.Commands.Countries;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Create;

/// <summary>
///     Представляет валидатор команды <see cref="CreateCountryCommand"/>
/// </summary>
public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator() =>
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(128);
}
