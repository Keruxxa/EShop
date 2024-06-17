using FluentValidation;

namespace EShop.Application.Features.Commands.Countries.Delete
{
    public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
            => RuleFor(command => command.Id).NotEmpty();
    }
}
