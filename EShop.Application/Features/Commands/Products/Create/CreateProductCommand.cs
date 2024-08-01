using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.Features.Commands.Products.Create
{
    /// <summary>
    ///     Представляет команду для создания товара
    /// </summary>
	public record CreateProductCommand(
        string Name,
        string? Description,
        DateTime? ReleaseDate,
        decimal Price,
        int CategoryId,
        int BrandId,
        int? CountryManufacturerId) : IRequest<Result<Guid>>;
}
