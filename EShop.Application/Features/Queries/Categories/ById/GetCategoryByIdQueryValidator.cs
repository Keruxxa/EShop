using FluentValidation;

namespace EShop.Application.Features.Queries.Categories.ById
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
            => RuleFor(command => command.Id).GreaterThan(0);
    }
}
