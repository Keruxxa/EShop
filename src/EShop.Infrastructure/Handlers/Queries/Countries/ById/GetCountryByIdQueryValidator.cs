using EShop.Application.CQRS.Queries.Countries;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Queries.Countries.ById;

/// <summary>
///     Представляет валидатор запроса <see cref="GetCountryByIdQuery"/>
/// </summary>
public class GetCountryByIdQueryValidator : AbstractValidator<GetCountryByIdQuery>
{
    public GetCountryByIdQueryValidator() =>
        RuleFor(query => query.Id).GreaterThan(0);
}
