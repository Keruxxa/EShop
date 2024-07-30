using FluentValidation;

namespace EShop.Application.Features.Queries.Categories.ById
{
    /// <summary>
    ///     Представляет валидатор запроса <see cref="GetCategoryByIdQuery"/>
    /// </summary>
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
            => RuleFor(command => command.Id).GreaterThan(0);
    }
}
