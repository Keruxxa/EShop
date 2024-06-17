using FluentValidation;

namespace EShop.Application.Features.Queries.Brands.ById
{
    public class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
    {
        public GetBrandByIdQueryValidator()
            => RuleFor(command => command.Id).GreaterThan(0);
    }
}
