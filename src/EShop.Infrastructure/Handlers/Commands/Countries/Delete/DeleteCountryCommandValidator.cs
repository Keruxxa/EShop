using EShop.Application.CQRS.Commands.Countries;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Delete;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteCountryCommand"/>
/// </summary>
public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
{
    public DeleteCountryCommandValidator() =>
        RuleFor(command => command.Id).GreaterThan(0);
}
