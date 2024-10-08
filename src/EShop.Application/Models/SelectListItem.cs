﻿using EShop.Domain.Entities;

namespace EShop.Application.Models;

/// <summary>
///     Представляет объект для отображения элемента в списке
/// </summary>
/// <typeparam name="TKey">Тип Id</typeparam>
public class SelectListItem<TKey> : EntityBase<TKey> where TKey : struct
{
    /// <summary>
    ///     Наименование
    /// </summary>
    public string Name { get; set; }


    public static SelectListItem<int> CreateItem(Country country)
    {
        return new SelectListItem<int>()
        {
            Id = country.Id,
            Name = country.Name
        };
    }


    public static SelectListItem<int> CreateItem(Brand brand)
    {
        return new SelectListItem<int>()
        {
            Id = brand.Id,
            Name = brand.Name
        };
    }


    public static SelectListItem<int> CreateItem(Category category)
    {
        return new SelectListItem<int>()
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}
