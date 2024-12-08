namespace EShop.Domain.Enums;

/// <summary>
///     Представляет перечисление, описывающее статус доставки товара
/// </summary>
public enum DeliveryStatus
{
    /// <summary>
    ///     Оплачен
    /// </summary>
    Payed = 1,

    /// <summary>
    ///     Отправлен
    /// </summary>
    Sent,

    /// <summary>
    ///     Получен
    /// </summary>
    Recieved
}
