using FluentValidation;

namespace EShop.Application.Features.Commands.Countries.Delete
{
    /// <summary>
    ///     Представляет валидатор команды <see cref="DeleteCountryCommand"/>
    /// </summary>
    public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
            => RuleFor(command => command.Id).NotEmpty();
    }
}
