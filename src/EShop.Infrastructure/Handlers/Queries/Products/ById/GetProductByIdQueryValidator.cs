using EShop.Application.CQRS.Queries.Products;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Queries.Products.ById;

/// <summary>
///     Представляет валидатор запроса <see cref="GetProductByIdQuery"/>
/// </summary>
public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator() =>
        RuleFor(query => query.Id).NotEmpty();
}
