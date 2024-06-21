﻿namespace EShop.Domain.Entities
{
    public class Category : EntityBase<int>
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Товары, относящиеся к данной категории
        /// </summary>
        public IReadOnlyCollection<Product> Products { get; }


        private Category() { }

        public Category(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Устанавливает значение наименования <see cref="Name"/>
        /// </summary>
        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}
