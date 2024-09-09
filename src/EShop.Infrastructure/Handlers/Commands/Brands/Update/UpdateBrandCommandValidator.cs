using EShop.Application.CQRS.Commands.Brands;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Update;

/// <summary>
///     Представляет валидатор команды <see cref="UpdateBrandCommand"/>
/// </summary>
public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator() =>
        RuleFor(command => command.Name).NotEmpty();
}
