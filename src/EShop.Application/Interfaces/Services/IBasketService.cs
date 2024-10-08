namespace EShop.Application.Interfaces.Services;

public interface IBasketService
{
    /// <summary>
    ///     Проверяет, существует ли корзина с данным Id
    /// </summary>
    Task<bool> IsBasketExistAsync(Guid id, CancellationToken cancellationToken);
}
