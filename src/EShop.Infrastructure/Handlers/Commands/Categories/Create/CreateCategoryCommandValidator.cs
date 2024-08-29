using EShop.Application.CQRS.Commands.Categories;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Categories.Create;

/// <summary>
///     Представляет валидатор команды <see cref="CreateCategoryCommand"/>
/// </summary>
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator() =>
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(128);
}