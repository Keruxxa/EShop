using FluentValidation;

namespace EShop.Application.Features.Commands.Brands.Delete
{
    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
            => RuleFor(command => command.Id).GreaterThan(0);
    }
}
