using EShop.Application.CQRS.Commands.Brands;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Delete;

/// <summary>
///     Представляет валидатор команды <see cref="DeleteBrandCommand"/>
/// </summary>
public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    public DeleteBrandCommandValidator() =>
        RuleFor(command => command.Id).GreaterThan(0);
}
