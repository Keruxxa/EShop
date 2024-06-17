using FluentValidation;

namespace EShop.Application.Features.Commands.Countries.Create
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
            => RuleFor(command => command.Name)
                   .NotEmpty()
                   .MaximumLength(128);
    }
}
