using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.CQRS.Queries.Reviews;

/// <summary>
///     Представляет запрос для получения списка отзывов пользователя
/// </summary>
public record GetReviewListByUserIdQuery(Guid UserId) : IRequest<Result<List<Review>, Error>>;
