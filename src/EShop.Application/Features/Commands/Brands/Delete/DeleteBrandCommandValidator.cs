using FluentValidation;

namespace EShop.Application.Features.Commands.Brands.Delete;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteBrandCommand"/>
/// </summary>
public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    public DeleteBrandCommandValidator()
        => RuleFor(command => command.Id).GreaterThan(0);
}
