using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Favorites;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Infrastructure.Handlers.Commands.Favorites.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="AddProductToFavoriteCommand"/>
/// </summary>
public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, Result<Unit, Error>>
{
    private readonly IFavoriteRepository _favoriteRepository;

    public DeleteFavoriteCommandHandler(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }


    public async Task<Result<Unit, Error>> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
    {
        var favorite = await _favoriteRepository.GetByIdAsync(request.Id, cancellationToken);

        if (favorite is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Favorite), request.Id), ErrorType.NotFound));
        }

        _favoriteRepository.Delete(favorite);

        var isSaved = await _favoriteRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
