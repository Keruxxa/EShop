﻿using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories;

public interface IBasketRepository
{
    /// <summary>
    ///     Получает корзину
    /// </summary>
    Task<Basket> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаляет корзину
    /// </summary>
    void Delete(Basket basket);

    /// <summary>
    ///     Сохраняет изменения контекста
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
