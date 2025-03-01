using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Review;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Queries.Reviews;

/// <summary>
///     Представляет запрос для получения списка отзывов пользователя
/// </summary>
public record GetReviewListByUserIdQuery(Guid UserId) : IRequest<Result<List<ReviewListItemByUserIdDto>, Error>>;
