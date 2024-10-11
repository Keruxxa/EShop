using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Favorites;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Favorites.DeleteProduct;

/// <summary>
///     Представляет обработчик команды <see cref="AddProductToFavoriteCommand"/>
/// </summary>
public class DeleteProductFromFavoriteCommandHandler : IRequestHandler<DeleteProductFromFavoriteCommand, Result<Unit, Error>>
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IProductService _productService;

    public DeleteProductFromFavoriteCommandHandler(IFavoriteRepository favoriteRepository, IProductService productService)
    {
        _favoriteRepository = favoriteRepository;
        _productService = productService;
    }


    public async Task<Result<Unit, Error>> Handle(DeleteProductFromFavoriteCommand request, CancellationToken cancellationToken)
    {
        if (!await _productService.IsProductExistAsync(request.ProductId, cancellationToken))
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Product), request.ProductId), ErrorType.NotFound));
        }

        var favorite = await _favoriteRepository.GetByIdAsync(request.Id, cancellationToken);

        if (favorite is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Favorite), request.Id), ErrorType.NotFound));
        }

        var isDeleted = favorite.DeleteProduct(request.ProductId);

        if (!isDeleted)
        {
            return Result.Failure<Unit, Error>(new Error(new BadRequestEntityError(PRODUCT_DOES_NOT_EXIST_IN_FAVORITE), ErrorType.BadRequest));
        }

        var isSaved = await _favoriteRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
