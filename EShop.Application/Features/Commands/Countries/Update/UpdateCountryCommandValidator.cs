using FluentValidation;

namespace EShop.Application.Features.Commands.Countries.Update
{
    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);

            RuleFor(command => command.Name).NotEmpty();
        }
    }
}
