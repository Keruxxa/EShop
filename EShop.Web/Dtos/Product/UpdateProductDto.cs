namespace EShop.Infrastructure.Dtos.Product
{
    /// <summary>
    ///     Представляет объект DTO для обновления товара
    /// </summary>
    /// <param name="Name"> Наименование </param>
    /// <param name="Description"> Описание </param>
    /// <param name="ReleaseDate"> Дата выпуска </param>
    /// <param name="Price"> Цена </param>
    /// <param name="CategoryId"> Id категории </param>
    /// <param name="CountryManufacturerId"> Id страны-производителя </param>
    public record UpdateProductDto(
        string Name,
        string? Description,
        DateTime? ReleaseDate,
        decimal Price,
        int CategoryId,
        int? CountryManufacturerId);
}
