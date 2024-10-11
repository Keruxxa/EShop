using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Favorites;

/// <summary>
///     Представляет команду для удаления товара из избранного
/// </summary>
public record DeleteProductFromFavoriteCommand(Guid Id, Guid ProductId) : IRequest<Result<Unit, Error>>;
