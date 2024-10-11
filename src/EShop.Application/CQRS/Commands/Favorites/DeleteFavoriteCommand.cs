using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Favorites;

/// <summary>
///     Представляет команду для добавления товара в избранное
/// </summary>
public record DeleteFavoriteCommand(Guid Id) : IRequest<Result<Unit, Error>>;
