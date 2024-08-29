using EShop.Application.CQRS.Commands.Categories;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Categories.Delete;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteCategoryCommand"/>
/// </summary>
public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator() =>
        RuleFor(command => command.Id).GreaterThan(0);
}
