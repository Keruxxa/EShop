using EShop.Application.CQRS.Queries.Brands;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Queries.Brands.ById;

/// <summary>
///     Представляет валидатор запроса <see cref="GetBrandByIdQuery"/>
/// </summary>
public class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdQueryValidator() =>
        RuleFor(query => query.Id).GreaterThan(0);
}
