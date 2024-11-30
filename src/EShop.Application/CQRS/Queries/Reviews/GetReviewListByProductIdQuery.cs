using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.CQRS.Queries.Reviews;

/// <summary>
///     Представляет запрос для получения списка отзывов товара
/// </summary>
public record GetReviewListByProductIdQuery(Guid ProductId) : IRequest<Result<List<Review>, Error>>;
