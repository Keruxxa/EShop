using FluentValidation;

namespace EShop.Application.Features.Queries.Products.ById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
            => RuleFor(command => command.Id).NotEmpty();
    }
}
