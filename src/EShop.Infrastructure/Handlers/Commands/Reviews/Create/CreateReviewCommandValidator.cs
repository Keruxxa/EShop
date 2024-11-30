using EShop.Application.CQRS.Commands.Reviews;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Reviews.Create;

/// <summary>
///     Представляет валидатор команды <see cref="CreateReviewCommand"/>
/// </summary>
public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(command => command.ProductId).NotEmpty();

        RuleFor(command => command.UserId).NotEmpty();
    }
}
