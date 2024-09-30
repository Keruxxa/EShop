using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Products;

/// <summary>
///     Представляет команду для обновления товара
/// </summary>
/// <param name="Name"> Наименование </param>
/// <param name="Description"> Описание </param>
/// <param name="ReleaseDate"> Дата выпуска </param>
/// <param name="Price"> Цена </param>
/// <param name="CategoryId"> Id категории </param>
/// <param name="BrandId"> Id бренда </param>
/// <param name="CountryManufacturerId"> Id страны-производителя </param>
public record UpdateProductCommand(
    Guid Id,
    string Name,
    string? Description,
    DateTime? ReleaseDate,
    decimal Price,
    int CategoryId,
    int BrandId,
    int? CountryManufacturerId) : IRequest<Result<Unit, Error>>;
