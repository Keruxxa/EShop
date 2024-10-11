using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Favorites;
using EShop.Application.Dtos.Favorite;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Favorites.ById;

/// <summary>
///     Представляет обработчик запроса <see cref="GetFavoriteByIdQuery"/>
/// </summary>
public class GetFavoriteByIdQueryHandler : IRequestHandler<GetFavoriteByIdQuery, Result<FavoriteDto, Error>>
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IMapper _mapper;

    public GetFavoriteByIdQueryHandler(IFavoriteRepository favoriteRepository, IMapper mapper)
    {
        _favoriteRepository = favoriteRepository;
        _mapper = mapper;
    }


    public async Task<Result<FavoriteDto, Error>> Handle(GetFavoriteByIdQuery request, CancellationToken cancellationToken)
    {
        var favorite = await _favoriteRepository.GetByIdAsync(request.Id, cancellationToken);

        if (favorite is null)
        {
            return Result.Failure<FavoriteDto, Error>(new Error(new NotFoundEntityError(nameof(Favorite), request.Id), ErrorType.NotFound));
        }

        var favoriteDto = _mapper.From(favorite).AdaptToType<FavoriteDto>();

        return Result.Success<FavoriteDto, Error>(favoriteDto);
    }
}
