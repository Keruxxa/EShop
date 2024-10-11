using EShop.Application.CQRS.Commands.Favorites;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Favorites.DeleteProduct;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteProductFromFavoriteCommand"/>
/// </summary>
public class DeleteProductFromFavoriteCommandValidator : AbstractValidator<DeleteProductFromFavoriteCommand>
{
}
