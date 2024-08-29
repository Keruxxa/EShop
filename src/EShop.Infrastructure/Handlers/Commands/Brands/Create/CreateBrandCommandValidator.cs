using EShop.Application.CQRS.Commands.Brands;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Create;

/// <summary>
///     Представляет валидатор команды <see cref="CreateBrandCommand"/>
/// </summary>
public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator() =>
        RuleFor(command => command.Name).NotEmpty();
}
