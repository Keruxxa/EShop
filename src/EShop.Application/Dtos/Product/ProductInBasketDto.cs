namespace EShop.Application.Dtos.Product;

/// <summary>
///     Представляет объект DTO товара в корзине
/// </summary>
/// <param name="Id"> Id </param>
/// <param name="Name"> Наименование </param>
/// <param name="Price"> Цена </param>
public record ProductInBasketDto(Guid Id, string Name, decimal Price, int Count);
