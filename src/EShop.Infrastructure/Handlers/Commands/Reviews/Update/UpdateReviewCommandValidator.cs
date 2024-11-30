using EShop.Application.CQRS.Commands.Reviews;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Reviews.Update;

/// <summary>
///     Представляет валидатор команды <see cref="CreateReviewCommand"/>
/// </summary>
public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(command => command.ProductId).NotEmpty();

        RuleFor(command => command.UserId).NotEmpty();

        RuleFor(command => command.Rating).ExclusiveBetween(0, 6);
    }
}
