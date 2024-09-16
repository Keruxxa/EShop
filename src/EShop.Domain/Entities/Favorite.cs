namespace EShop.Domain.Entities;

/// <summary>
///     Представляет коллекцию избранного
/// </summary>
public class Favorite : EntityBase<Guid>
{
    /// <summary>
    ///     Товары, содержащиеся в коллекции избранного
    /// </summary>
    private List<Product> _products = [];

    /// <summary>
    ///     Товары, содержащиеся в коллекции избранного
    /// </summary>
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();


    private Favorite() { }

    public Favorite(Guid userId)
    {
        Id = userId;
    }

    /// <summary>
    ///     Добавляет объект <see cref="Product"/> в коллекцию <see cref="Products"/>
    /// </summary>
    public void AddItem(Product product)
    {
        if (!_products.Contains(product))
        {
            _products.Add(product);
        }
    }

    /// <summary>
    ///     Удаляет объект <see cref="Product"/> из коллекции <see cref="Products"/>
    /// </summary>
    public bool RemoveItem(Product product)
    {
        return _products.Remove(product);
    }
}
