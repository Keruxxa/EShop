using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Reviews;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Infrastructure.Handlers.Commands.Reviews.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateReviewCommand"/>
/// </summary>
public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<Review, Error>>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUserService _userService;
    private readonly IProductService _productService;

    public CreateReviewCommandHandler(IReviewRepository reviewRepository, IUserService userService, IProductService productService)
    {
        _reviewRepository = reviewRepository;
        _userService = userService;
        _productService = productService;
    }


    public async Task<Result<Review, Error>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        if (!await _productService.IsProductExistAsync(request.ProductId, cancellationToken))
        {
            return Result.Failure<Review, Error>(new Error(new NotFoundEntityError(nameof(Product), request.ProductId), ErrorType.NotFound));
        }

        if (!await _userService.IsUserExistAsync(request.UserId, cancellationToken))
        {
            return Result.Failure<Review, Error>(new Error(new NotFoundEntityError(nameof(User), request.UserId), ErrorType.NotFound));
        }

        var review = new Review(request.ProductId, request.UserId, request.Rating, request.Text);

        var reviewEntity = _reviewRepository.Add(review);

        var isSaved = await _reviewRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Review, Error>(reviewEntity)
            : Result.Failure<Review, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
