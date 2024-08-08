namespace EShop.Domain.Entities;

/// <summary>
///     Представляет бренд
/// </summary>
public class Brand : EntityBase<int>
{
    /// <summary>
    ///     Товары, относящиеся к данному бренду
    /// </summary>
    private readonly List<BrandProducts> _brandProducts = [];

    /// <summary>
    ///     Наименование
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Товары, относящиеся к данному бренду
    /// </summary>
    public IReadOnlyCollection<BrandProducts> BrandProducts => _brandProducts.AsReadOnly();


    private Brand() { }

    public Brand(string name)
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