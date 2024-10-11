namespace EShop.Application.Dtos.Product;

/// <summary>
///     Представляет объект DTO товара в избранном
/// </summary>
/// <param name="Id"> Id </param>
/// <param name="Name"> Наименование </param>
/// <param name="Price"> Цена </param>
public record ProductInFavoriteDto(Guid Id, string Name, decimal Price);
