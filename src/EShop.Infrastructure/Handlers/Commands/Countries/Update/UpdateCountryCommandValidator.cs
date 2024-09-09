using EShop.Application.CQRS.Commands.Countries;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Update;

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