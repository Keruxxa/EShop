using EShop.Application.CQRS.Queries.Reviews;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Queries.Reviews.ListByProductId;

/// <summary>
///     Представялет валидатор запроса <see cref="GetReviewListByProductIdQuery"/>
/// </summary>
public class GetReviewListByProductIdQueryValidator : AbstractValidator<GetReviewListByProductIdQuery>
{
    public GetReviewListByProductIdQueryValidator()
        => RuleFor(query => query.ProductId).NotEmpty();
}
