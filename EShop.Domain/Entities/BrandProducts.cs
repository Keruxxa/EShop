namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет промежуточную сущность, связывающую сущности 
    ///     <see cref="Brand"/> и <see cref="Product"/>
    /// </summary>
    public class BrandProducts
    {

        /// <summary>
        ///     Id бренда
        /// </summary>
        public int BrandId { get; private set; }

        /// <summary>
        ///     Id товара
        /// </summary>
        public Guid ProductId { get; set; }


        private BrandProducts() { }


        public BrandProducts(int brandId, Guid productId)
        {
            BrandId = brandId;
            ProductId = productId;
        }
    }
}
