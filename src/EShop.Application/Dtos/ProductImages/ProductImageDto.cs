namespace EShop.Application.Dtos.ProductImages;

/// <summary>
///     Представляет объект DTO для изображения товара
/// </summary>
/// <param name="Uri"> Uri </param>
/// <param name="IsMain"> Главное изображение </param>
/// <param name="Order"> Порядок </param>
public record ProductImageDto(string Uri, bool IsMain, byte Order);
