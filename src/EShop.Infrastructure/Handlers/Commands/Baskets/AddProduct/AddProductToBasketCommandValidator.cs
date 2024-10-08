using EShop.Application.CQRS.Commands.Baskets;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Baskets.AddProduct;

/// <summary>
///     Представляет валидатор команды <see cref="AddProductToBasketCommand"/>
/// </summary>
public class AddProductToBasketCommandValidator : AbstractValidator<AddProductToBasketCommand>
{
    public AddProductToBasketCommandValidator()
    {
        RuleFor(command => command.BasketId).NotEmpty();
        RuleFor(command => command.ProductId).NotEmpty();
    }
}
