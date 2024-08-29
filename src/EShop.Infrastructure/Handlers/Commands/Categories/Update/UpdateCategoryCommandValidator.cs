using EShop.Application.CQRS.Commands.Categories;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Categories.Update;

/// <summary>
///     Представляет валидатор команды <see cref="UpdateCategoryCommand"/>
/// </summary>
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(command => command.Id).GreaterThan(0);

        RuleFor(command => command.Name).NotEmpty();
    }
}
