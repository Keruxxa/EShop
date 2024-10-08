using EShop.Application.CQRS.Commands.Baskets;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Baskets.Create;

/// <summary>
///     Представляет валидатор команды <see cref="CreateBasketCommand"/>
/// </summary>
public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator() =>
        RuleFor(command => command.Id).NotEmpty();
}
