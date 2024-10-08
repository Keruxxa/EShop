using EShop.Application.CQRS.Queries.Baskets;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Queries.Baskets.ById;

/// <summary>
///     Представляет валидатор запроса <see cref="GetBasketByIdQuery"/>
/// </summary>
public class GetBasketByIdQueryValidator : AbstractValidator<GetBasketByIdQuery>
{
    public GetBasketByIdQueryValidator() =>
        RuleFor(query => query.Id).NotEmpty();
}
