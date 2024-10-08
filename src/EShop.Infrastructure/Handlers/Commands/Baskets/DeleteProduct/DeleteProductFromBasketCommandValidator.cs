using EShop.Application.CQRS.Commands.Baskets;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Baskets.DeleteProduct;

/// <summary>
///     Представляет валидатор команды <see cref="AddProductToBasketCommand"/>
/// </summary>
public class DeleteProductFromBasketCommandValidator : AbstractValidator<AddProductToBasketCommand>
{
    public DeleteProductFromBasketCommandValidator()
    {
        RuleFor(command => command.BasketId).NotEmpty();
        RuleFor(command => command.ProductId).NotEmpty();
    }
}
