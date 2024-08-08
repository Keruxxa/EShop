using FluentValidation;

namespace EShop.Application.Features.Commands.Countries.Update;

/// <summary>
///     Представляет валидатор команды <see cref="UpdateCountryCommand"/>
/// </summary>
public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(command => command.Id).GreaterThan(0);

        RuleFor(command => command.Name).NotEmpty();
    }
}
