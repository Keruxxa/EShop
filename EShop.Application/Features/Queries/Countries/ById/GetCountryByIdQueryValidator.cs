using FluentValidation;

namespace EShop.Application.Features.Queries.Countries.ById
{
    /// <summary>
    ///     Представляет валидатор запроса <see cref="GetCountryByIdQuery"/>
    /// </summary>
    public class GetCountryByIdQueryValidator : AbstractValidator<GetCountryByIdQuery>
    {
        public GetCountryByIdQueryValidator()
            => RuleFor(command => command.Id).GreaterThan(0);
    }
}
