using EShop.Application.CQRS.Commands.Reviews;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Reviews.Delete;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteReviewCommand"/>
/// </summary>
public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(command => command.ProductId).NotEmpty();

        RuleFor(command => command.UserId).NotEmpty();
    }
}
