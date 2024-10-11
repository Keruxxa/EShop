using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Favorites;

/// <summary>
///     Представляет команду для добавления товара в избранное
/// </summary>
public record AddProductToFavoriteCommand(Guid Id, Guid ProductId) : IRequest<Result<Unit, Error>>;
