using FluentValidation;

namespace EShop.Application.Features.Queries.Products.ById
{
    /// <summary>
    ///     Представляет валидатор запроса <see cref="GetProductByIdQuery"/>
    /// </summary>
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
            => RuleFor(query => query.Id).NotEmpty();
    }
}
