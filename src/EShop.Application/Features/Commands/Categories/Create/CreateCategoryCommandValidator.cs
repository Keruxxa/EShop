using FluentValidation;

namespace EShop.Application.Features.Commands.Categories.Create;

/// <summary>
///     Представляет валидатор команды <see cref="CreateCategoryCommand"/>
/// </summary>
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
        => RuleFor(command => command.Name)
               .NotEmpty()
               .MaximumLength(128);
}
