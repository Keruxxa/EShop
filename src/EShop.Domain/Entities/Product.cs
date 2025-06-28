namespace EShop.Domain.Entities;

/// <summary>
///     Представляет товар
/// </summary>
public class Product : EntityBase<Guid>
{
    /// <summary>
    ///     Отзывы товара
    /// </summary>
    private readonly List<Review> _reviews = [];


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
    public decimal? Rating
    {
        get
        {
            if (_reviews.Count == 0)
            {
                return null;
            }

            var sum = _reviews.Sum(x => (decimal)x.Rating);

            return sum / _reviews.Count;
        }
    }

    /// <summary>
    ///     Id категории
    /// </summary>
    public int CategoryId { get; private set; }

    /// <summary>
    ///     Категория
    /// </summary>
    public Category? Category { get; private set; }

    /// <summary>
    ///     Id бренда
    /// </summary>
    public int BrandId { get; private set; }

    /// <summary>
    ///     Бренд
    /// </summary>
    public Brand? Brand { get; private set; }

    /// <summary>
    ///     Id страны-производителя
    /// </summary>
    public int? CountryManufacturerId { get; private set; }

    /// <summary>
    ///     Страна-производитель
    /// </summary>
    public Country? CountryManufacturer { get; private set; }

    /// <summary>
    ///     Отзывы товара
    /// </summary>
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

    /// <summary>
    ///     Количество отзывов
    /// </summary>
    public int ReviewCount => _reviews.Count;


    public Product(
        string name,
        int brandId,
        int categoryId,
        decimal price,
        string? description,
        DateTime? releaseDate,
        int? countryManufacturerId)
    {
        Name = name;
        BrandId = brandId;
        CategoryId = categoryId;
        Price = price;
        Description = description;
        ReleaseDate = releaseDate?.ToUniversalTime();
        CountryManufacturerId = countryManufacturerId;
    }

    /// <summary>
    ///     Обновляет поля сущности
    /// </summary>
    public void UpdateEntity(
        string name,
        int brandId,
        int categoryId,
        decimal price,
        string? description,
        DateTime? releaseDate,
        int? countryManufacturerId)
    {
        Name = name;
        BrandId = brandId;
        CategoryId = categoryId;
        Price = price;
        Description = description;
        ReleaseDate = releaseDate;
        CountryManufacturerId = countryManufacturerId;
    }
}