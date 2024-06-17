using FluentValidation;

namespace EShop.Application.Features.Queries.Countries.ById
{
    public class GetCountryByIdQueryValidator : AbstractValidator<GetCountryByIdQuery>
    {
        public GetCountryByIdQueryValidator()
            => RuleFor(command => command.Id).GreaterThan(0);
    }
}
