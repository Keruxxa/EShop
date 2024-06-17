using FluentValidation;

namespace EShop.Application.Features.Commands.Products.Delete
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
            => RuleFor(command => command.Id).NotEmpty();
    }
}
