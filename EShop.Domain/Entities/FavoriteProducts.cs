namespace EShop.Domain.Entities;

/// <summary>
///     Представляет промежуточную сущность, связывающую сущности 
///     <see cref="Favorite"/> и <see cref="Product"/>
/// </summary>
public class FavoriteProducts
{
    /// <summary>
    ///     Id коллекции избранного
    /// </summary>
    public Guid FavoriteId { get; private set; }

    /// <summary>
    ///     Id товара
    /// </summary>
    public Guid ProductId { get; private set; }


    private FavoriteProducts() { }


    /// <param name="favoriteId">Id коллекции избранного</param>
    /// <param name="productId">Id товара</param>
    public FavoriteProducts(Guid favoriteId, Guid productId)
    {
        FavoriteId = favoriteId;
        ProductId = productId;
    }
}
