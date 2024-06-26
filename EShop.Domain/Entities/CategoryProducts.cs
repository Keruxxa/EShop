namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет промежуточную сущность, связывающую сущности 
    ///     <see cref="Category"/> и <see cref="Product"/>
    /// </summary>
    public class CategoryProducts
    {
        /// <summary>
        ///     Id категории
        /// </summary>
        public int CategoryId { get; private set; }

        /// <summary>
        ///     Id товара
        /// </summary>
        public Guid ProductId { get; private set; }


        public CategoryProducts() { }


        /// <param name="categoryId">Id категории</param>
        /// <param name="productId">Id товара</param>
        public CategoryProducts(int categoryId, Guid productId)
        {
            CategoryId = categoryId;
            ProductId = productId;
        }
    }
}
