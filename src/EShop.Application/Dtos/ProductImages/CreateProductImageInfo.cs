namespace EShop.Application.Dtos.ProductImages;

/// <summary>
///     Представляет объект DTO об информации изображения товара
/// </summary>
/// <param name="FileName"> Имя файл </param>
/// <param name="IsMain"> Главное изображение </param>
/// <param name="Order"> Порядок </param>
public record CreateProductImageInfo(string FileName, bool IsMain, byte Order);
