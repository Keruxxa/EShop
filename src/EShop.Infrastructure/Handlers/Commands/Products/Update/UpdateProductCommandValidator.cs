using EShop.Application.CQRS.Commands.Products;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Commands.Products.Update;

/// <summary>
///     Представляет валидатор команды <see cref="UpdateProductCommand"/>
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();

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

