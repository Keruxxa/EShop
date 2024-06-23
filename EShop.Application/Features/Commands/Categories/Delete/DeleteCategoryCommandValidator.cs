using FluentValidation;

namespace EShop.Application.Features.Commands.Categories.Delete
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
            => RuleFor(command => command.Id).GreaterThan(0);
    }
}
