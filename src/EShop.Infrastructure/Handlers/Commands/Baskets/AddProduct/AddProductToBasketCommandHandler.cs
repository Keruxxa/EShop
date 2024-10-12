using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Baskets;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Infrastructure.Handlers.Commands.Baskets.AddProduct;

/// <summary>
///     Представляет обработчик команды <see cref="AddProductToBasketCommand"/>
/// </summary>
public class AddProductToBasketCommandHandler : IRequestHandler<AddProductToBasketCommand, Result<Unit, Error>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IProductService _productService;

    public AddProductToBasketCommandHandler(IBasketRepository basketRepository, IProductService productService)
    {
        _basketRepository = basketRepository;
        _productService = productService;
    }


    public async Task<Result<Unit, Error>> Handle(AddProductToBasketCommand request, CancellationToken cancellationToken)
    {
        if (!await _productService.IsProductExistAsync(request.ProductId, cancellationToken))
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Product), request.ProductId), ErrorType.NotFound));
        }

        var basket = await _basketRepository.GetByIdAsync(request.BasketId, cancellationToken);

        if (basket is null)
        {
            basket = new Basket(request.BasketId);
            _basketRepository.Create(basket);
        }

        basket.AddItem(request.ProductId);

        var isSaved = await _basketRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
