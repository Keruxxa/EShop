using FluentValidation;

namespace EShop.Application.Features.Queries.Brands.ById
{
    /// <summary>
    ///     Представляет валидатор запроса <see cref="GetBrandByIdQuery"/>
    /// </summary>
    public class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
    {
        public GetBrandByIdQueryValidator()
            => RuleFor(command => command.Id).GreaterThan(0);
    }
}
