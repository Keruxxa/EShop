using FluentValidation;

namespace EShop.Application.Features.Commands.Brands.Create
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
            => RuleFor(command => command.Name).NotEmpty();
    }
}
