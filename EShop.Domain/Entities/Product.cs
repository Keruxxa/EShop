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
        public string Name { get; private set; }

        /// <summary>
        ///     Описание
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        ///     Дата выпуска
        /// </summary>
        public DateTime? ReleaseDate { get; private set; }

        /// <summary>
        ///     Цена
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        ///     Рейтинг
        /// </summary>
        public decimal? Rating { get; private set; }

        /// <summary>
        ///     Id категории
        /// </summary>
        public int CategoryId { get; private set; }

        /// <summary>
        ///     Категория
        /// </summary>
        public Category Category { get; private set; }

        /// <summary>
        ///     Id бренда
        /// </summary>
        public int BrandId { get; private set; }

        /// <summary>
        ///     Бренд
        /// </summary>
        public Brand Brand { get; private set; }

        /// <summary>
        ///     Id страны-производителя
        /// </summary>
        public int? CountryManufacturerId { get; private set; }

        /// <summary>
        ///     Страна-производитель
        /// </summary>
        public Country CountryManufacturer { get; private set; }

        /// <summary>
        ///     Отзывы
        /// </summary>
        public IReadOnlyCollection<Review>? Reviews { get; }


        private Product() { }


        public Product(string name, string? description, DateTime? releaseDate,
            decimal price, int categoryId, int brandId, int? countryManufacturerId)
        {
            Name = name;
            Description = description;
            ReleaseDate = releaseDate?.ToUniversalTime();
            Price = price;
            CategoryId = categoryId;
            BrandId = brandId;
            CountryManufacturerId = countryManufacturerId;
        }


        public void UpdateEntity(string name, string? description, DateTime? releaseDate,
            decimal price, int categoryId, int brandId, int? countryManufacturerId)
        {
            Name = name;
            Description = description;
            ReleaseDate = releaseDate;
            Price = price;
            CategoryId = categoryId;
            BrandId = brandId;
            CountryManufacturerId = countryManufacturerId;
        }
    }
}