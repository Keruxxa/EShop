using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Favorite;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Queries.Favorites;

public record GetFavoriteByIdQuery(Guid Id) : IRequest<Result<FavoriteDto, Error>>;
