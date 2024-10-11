using EShop.Application.CQRS.Commands.Favorites;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Favorites.Delete;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteFavoriteCommand"/>
/// </summary>
public class DeleteFavoriteCommandValidator : AbstractValidator<DeleteFavoriteCommand>
{
    public DeleteFavoriteCommandValidator()
        => RuleFor(command => command.Id).NotEmpty();
}
