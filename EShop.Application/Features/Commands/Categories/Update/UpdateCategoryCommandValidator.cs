using FluentValidation;

namespace EShop.Application.Features.Commands.Categories.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);

            RuleFor(command => command.Name).NotEmpty();
        }
    }
}
