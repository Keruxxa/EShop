namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет товар
    /// </summary>
    public class Product : EntityBase<Guid>
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Дата выпуска
        /// </summary>
        public DateTime? RealiseDate { get; }

        /// <summary>
        ///     Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Рейтинг
        /// </summary>
        public decimal? Rating { get; set; }

        /// <summary>
        ///     Id категории
        /// </summary>
        public int CategoryId { get; }

        /// <summary>
        ///     Категория
        /// </summary>
        public Category Category { get; }

        /// <summary>
        ///     Id страны-производителя
        /// </summary>
        public int? CountryManufacturerId { get; }

        /// <summary>
        ///     Страна-производитель
        /// </summary>
        public Country CountryManufacturer { get; }

        /// <summary>
        ///     Отзывы
        /// </summary>
        public IReadOnlyCollection<Review>? Reviews { get; }


        private Product() { }
    }
}