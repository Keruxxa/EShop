using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Review;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Queries.Reviews;

/// <summary>
///     Представляет запрос для получения списка отзывов товара
/// </summary>
public record GetReviewListByProductIdQuery(Guid ProductId) : IRequest<Result<List<ReviewListItemByProductIdDto>, Error>>;
