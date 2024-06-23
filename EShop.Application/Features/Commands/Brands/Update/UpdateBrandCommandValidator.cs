using FluentValidation;

namespace EShop.Application.Features.Commands.Brands.Update
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
            => RuleFor(command => command.Name).NotEmpty();
    }
}
