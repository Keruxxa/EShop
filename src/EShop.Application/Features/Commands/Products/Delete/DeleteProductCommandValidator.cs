using FluentValidation;

namespace EShop.Application.Features.Commands.Products.Delete;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteProductCommand"/>
/// </summary>
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
        => RuleFor(command => command.Id).NotEmpty();
}
