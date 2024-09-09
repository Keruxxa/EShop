using EShop.Application.CQRS.Commands.Products;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Products.Create;

/// <summary>
///     Представляет валидатор команды <see cref="CreateProductCommand"/>
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(command => command.Description)
            .MaximumLength(256);

        RuleFor(command => command.ReleaseDate)
            .GreaterThan(new DateTime(2020, 1, 1))
            .When(command => command.ReleaseDate.HasValue);

        RuleFor(command => command.Price)
            .GreaterThan(0);

        RuleFor(command => command.CategoryId)
            .GreaterThan(0);

        RuleFor(command => command.BrandId)
            .GreaterThan(0);

        RuleFor(command => command.CountryManufacturerId)
            .GreaterThan(0)
            .When(command => command.CountryManufacturerId.HasValue);
    }
}
