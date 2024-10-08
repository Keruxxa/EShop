using EShop.Application.CQRS.Commands.Baskets;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Baskets.Delete;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteBasketCommand"/>
/// </summary>
public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator() =>
        RuleFor(command => command.Id).NotEmpty();
}
