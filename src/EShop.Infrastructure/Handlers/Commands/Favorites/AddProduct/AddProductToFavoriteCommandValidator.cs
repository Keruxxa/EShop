using EShop.Application.CQRS.Commands.Favorites;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Favorites.AddProduct;

/// <summary>
///     Представляет валидатор команды <see cref="AddProductToFavoriteCommand"/>
/// </summary>
public class AddProductToFavoriteCommandValidator : AbstractValidator<AddProductToFavoriteCommand>
{
    public AddProductToFavoriteCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
        RuleFor(command => command.ProductId).NotEmpty();
    }
}
