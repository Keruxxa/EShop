using FluentValidation;

namespace EShop.Application.Features.Commands.Categories.Delete;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteCategoryCommand"/>
/// </summary>
public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
        => RuleFor(command => command.Id).GreaterThan(0);
}
