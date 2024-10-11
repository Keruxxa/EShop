namespace EShop.Domain.Entities;

/// <summary>
///     Представляет коллекцию избранного
/// </summary>
public class Favorite : EntityBase<Guid>
{
    /// <summary>
    ///     Товары, содержащиеся в коллекции избранного
    /// </summary>
    private List<FavoriteProducts> _favoriteProducts = [];

    /// <summary>
    ///     Товары, содержащиеся в коллекции избранного
    /// </summary>
    public IReadOnlyCollection<FavoriteProducts> FavoriteProducts => _favoriteProducts;


    private Favorite() { }

    public Favorite(Guid userId)
    {
        Id = userId;
    }

    /// <summary>
    ///     Добавляет объект <see cref="Product"/> в коллекцию <see cref="FavoriteProducts"/>
    /// </summary>
    public bool AddProduct(Guid productId)
    {
        var favoriteProductExists = _favoriteProducts.FirstOrDefault(favoriteProduct =>
            favoriteProduct.FavoriteId == Id &&
            favoriteProduct.ProductId == productId);

        if (favoriteProductExists is null)
        {
            _favoriteProducts.Add(new FavoriteProducts(Id, productId));

            return true;
        }

        return false;
    }

    /// <summary>
    ///     Удаляет объект <see cref="Product"/> из коллекции <see cref="FavoriteProducts"/>
    /// </summary>
    public bool DeleteProduct(Guid productId)
    {
        var favoriteProductExists = _favoriteProducts.FirstOrDefault(favoriteProduct =>
            favoriteProduct.FavoriteId == Id &&
            favoriteProduct.ProductId == productId);

        if (favoriteProductExists is not null)
        {
            _favoriteProducts.Remove(new FavoriteProducts(Id, productId));

            return true;
        }

        return false;
    }
}
