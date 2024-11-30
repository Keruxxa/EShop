using EShop.Application.CQRS.Queries.Reviews;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Queries.Reviews.ListByUserId;

/// <summary>
///     Представялет валидатор запроса <see cref="GetReviewListByUserIdQuery"/>
/// </summary>
public class GetListByUserIdQueryValidator : AbstractValidator<GetReviewListByUserIdQuery>
{
    public GetListByUserIdQueryValidator()
        => RuleFor(query => query.UserId).NotEmpty();
}
