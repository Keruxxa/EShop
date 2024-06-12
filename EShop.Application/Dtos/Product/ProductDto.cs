namespace EShop.Application.Dtos.Product
{
    /// <summary>
    ///     Представляет объект DTO товара
    /// </summary>
    /// <param name="Name"> Наименование </param>
    /// <param name="Description"> Описание </param>
    /// <param name="ReleaseDate"> Дата выпуска </param>
    /// <param name="Price"> Цена </param>
    /// <param name="CategoryName"> Наименование категории </param>
    /// <param name="CountryManufacturerName"> Наименование страны-производителя </param>
    public record ProductDto(
        string Name,
        string? Description,
        DateTime? ReleaseDate,
        decimal Price,
        string CategoryName,
        string CountryManufacturerName);
}
