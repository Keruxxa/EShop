namespace EShop.Application.Interfaces.Services;

public interface IOrderService
{
    /// <summary>
    ///     Определяет, доступны ли все товары для заказа
    /// </summary>
    Task<bool> IsAllProductsExistAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken);
}
