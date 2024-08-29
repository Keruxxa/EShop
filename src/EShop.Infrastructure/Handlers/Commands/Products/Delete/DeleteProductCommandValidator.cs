using EShop.Application.CQRS.Commands.Products;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Products.Delete;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteProductCommand"/>
/// </summary>
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator() =>
        RuleFor(command => command.Id).NotEmpty();
}
