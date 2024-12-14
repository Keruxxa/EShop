using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Reviews;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Infrastructure.Handlers.Commands.Reviews.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateReviewCommand"/>
/// </summary>
public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Result<Unit, Error>>
{
    private readonly IReviewRepository _reviewRepository;

    public UpdateReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }


    public async Task<Result<Unit, Error>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetByIdAsync(request.ProductId, request.UserId, cancellationToken);

        if (review is null)
        {
            return Result.Failure<Unit, Error>(new Error(
                new NotFoundEntityError(nameof(Review), new { request.ProductId, request.UserId }),
                ErrorType.NotFound));
        }

        if (review.UserId != request.UserId)
        {
            return Result.Failure<Unit, Error>(new Error(
                new ForbiddenEntityError(request.UserId),
                ErrorType.Forbidden));
        }

        review.UpdateEntity(request.Rating, request.Text);

        _reviewRepository.Update(review);

        var isSaved = await _reviewRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
