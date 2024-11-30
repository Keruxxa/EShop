using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Reviews;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Reviews.ListByProductId;

/// <summary>
///     Представялет обработчик запроса <see cref="GetReviewListByProductIdQuery"/>
/// </summary>
public class GetReviewListByProductIdQueryHandler : IRequestHandler<GetReviewListByProductIdQuery, Result<List<Review>, Error>>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IProductService _productService;

    public GetReviewListByProductIdQueryHandler(IReviewRepository reviewRepository, IProductService productService)
    {
        _reviewRepository = reviewRepository;
        _productService = productService;
    }


    public async Task<Result<List<Review>, Error>> Handle(GetReviewListByProductIdQuery request, CancellationToken cancellationToken)
    {
        if (!await _productService.IsProductExistAsync(request.ProductId, cancellationToken))
        {
            return Result.Failure<List<Review>, Error>(new Error(new NotFoundEntityError(nameof(Product), request.ProductId), ErrorType.NotFound));
        }

        var reviews = await _reviewRepository.GetListByProductIdAsync(request.ProductId, cancellationToken);

        return Result.Success<List<Review>, Error>(reviews);
    }
}
