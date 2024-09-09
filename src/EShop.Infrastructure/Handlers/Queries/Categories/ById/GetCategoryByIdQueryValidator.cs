using EShop.Application.CQRS.Queries.Categories;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Queries.Categories.ById;

/// <summary>
///     Представляет валидатор запроса <see cref="GetCategoryByIdQuery"/>
/// </summary>
public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator() =>
        RuleFor(query => query.Id).GreaterThan(0);
}
