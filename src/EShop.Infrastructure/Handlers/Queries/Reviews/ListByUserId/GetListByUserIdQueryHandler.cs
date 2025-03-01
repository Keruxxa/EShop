using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Reviews;
using EShop.Application.Dtos.Review;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Queries.Reviews.ListByUserId;

/// <summary>
///     Представялет обработчик запроса <see cref="GetReviewListByUserIdQuery"/>
/// </summary>
public class GetListByUserIdQueryHandler : IRequestHandler<GetReviewListByUserIdQuery, Result<List<ReviewListItemByUserIdDto>, Error>>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUserService _userService;

    public GetListByUserIdQueryHandler(IReviewRepository reviewRepository, IUserService userService)
    {
        _reviewRepository = reviewRepository;
        _userService = userService;
    }


    public async Task<Result<List<ReviewListItemByUserIdDto>, Error>> Handle(GetReviewListByUserIdQuery request, CancellationToken cancellationToken)
    {
        if (!await _userService.IsUserExistAsync(request.UserId, cancellationToken))
        {
            return Result.Failure<List<ReviewListItemByUserIdDto>, Error>(
                new Error(new NotFoundEntityError(nameof(User), request.UserId), ErrorType.NotFound));
        }

        var reviews = _reviewRepository.GetListByUserId(request.UserId);

        var reviewListByUserIdDtos = await reviews.Select(review => review.Adapt<ReviewListItemByUserIdDto>()).ToListAsync(cancellationToken);

        return Result.Success<List<ReviewListItemByUserIdDto>, Error>(reviewListByUserIdDtos);
    }
}
