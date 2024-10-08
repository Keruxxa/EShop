﻿namespace EShop.Domain.Entities;

/// <summary>
///     Представляет отдельный товар корзины
/// </summary>
public class BasketItem
{
    /// <summary>
    ///     Id корзины, с которой связан товар
    /// </summary>
    public Guid BasketId { get; }

    /// <summary>
    ///     Id товара
    /// </summary>
    public Guid ProductId { get; }

    /// <summary>
    ///     Товар
    /// </summary>
    public Product Product { get; }

    /// <summary>
    ///     Количество товара в корзине
    /// </summary>
    public int Count { get; private set; }


    private BasketItem() { }

    public BasketItem(Guid basketId, Guid productId)
    {
        BasketId = basketId;
        ProductId = productId;
        Count = 1;
    }


    /// <summary>
    ///     Увеличивает на единицу количество товара в корзине
    /// </summary>
    public void IncrementItemCount() => Count++;

    /// <summary>
    ///     Уменьшает на единицу количество товара в корзине
    /// </summary>
    public bool DecrementItemCount()
    {
        if (Count > 0)
        {
            Count--;
            return true;
        }

        return false;
    }
}
