using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Baskets;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Baskets.DeleteProduct;

public class DeleteProductFromBasketCommandHandler : IRequestHandler<DeleteProductFromBasketCommand, Result<Unit, Error>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IProductService _productService;

    public DeleteProductFromBasketCommandHandler(IBasketRepository basketRepository, IProductService productService)
    {
        _basketRepository = basketRepository;
        _productService = productService;
    }


    public async Task<Result<Unit, Error>> Handle(DeleteProductFromBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetByIdAsync(request.BasketId, cancellationToken);

        if (basket is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Basket), request.BasketId), ErrorType.NotFound));
        }

        if (!await _productService.IsProductExistAsync(request.ProductId, cancellationToken))
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Product), request.ProductId), ErrorType.NotFound));
        }

        var isDeleted = basket.DeleteItem(new BasketItem(request.BasketId, request.ProductId));

        if (!isDeleted)
        {
            return Result.Failure<Unit, Error>(new Error(new BadRequestEntityError(PRODUCT_DOES_NOT_EXIST_IN_BASKET), ErrorType.BadRequest));
        }

        var isSaved = await _basketRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
