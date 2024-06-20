using FluentValidation;

namespace EShop.Application.Features.Commands.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
            => RuleFor(command => command.Name).NotEmpty();
    }
}
