using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Favorites;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Favorites.AddProduct;

/// <summary>
///     Представляет обработчик команды <see cref="AddProductToFavoriteCommand"/>
/// </summary>
public class AddProductToFavoriteCommandHandler : IRequestHandler<AddProductToFavoriteCommand, Result<Unit, Error>>
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IUserService _userService;
    private readonly IProductService _productService;

    public AddProductToFavoriteCommandHandler(IFavoriteRepository favoriteRepository, IUserService userService, IProductService productService)
    {
        _favoriteRepository = favoriteRepository;
        _userService = userService;
        _productService = productService;
    }


    public async Task<Result<Unit, Error>> Handle(AddProductToFavoriteCommand request, CancellationToken cancellationToken)
    {
        if (!await _productService.IsProductExistAsync(request.ProductId, cancellationToken))
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Product), request.ProductId), ErrorType.NotFound));
        }

        var favorite = await _favoriteRepository.GetByIdAsync(request.Id, cancellationToken);

        if (favorite is null)
        {
            favorite = new Favorite(request.Id);
            _favoriteRepository.Create(favorite);
        }

        var isAdded = favorite.AddProduct(request.ProductId);

        if (!isAdded)
        {
            return Result.Failure<Unit, Error>(new Error(new BadRequestEntityError(PRODUCT_ALLREADY_EXIST_IN_FAVORITE), ErrorType.BadRequest));
        }

        var isSaved = await _favoriteRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
