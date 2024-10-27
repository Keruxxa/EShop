using EShop.Application.CQRS.Queries.Categories;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Queries.Categories.HierarchyById;

public class GetCategoriesHierarchyByIdQueryValidator : AbstractValidator<GetCategoriesHierarchyByIdQuery>
{
    public GetCategoriesHierarchyByIdQueryValidator()
        => RuleFor(query => query.Id).GreaterThan(0);
}
