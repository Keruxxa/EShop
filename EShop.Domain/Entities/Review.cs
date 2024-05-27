namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет отзыв
    /// </summary>
    public class Review : EntityBase<int>
    {
        /// <summary>
        ///     Id товара, связанного с отзывом
        /// </summary>
        public Guid ProductId { get; }

        /// <summary>
        ///     Рейтинг
        /// </summary>
        public int Rating { get; }

        /// <summary>
        ///     Текстовая часть отзыва
        /// </summary>
        public string? Text { get; }

        /// <summary>
        ///     Товар, связанный с отзывом
        /// </summary>
        public Product Product { get; }


        private Review() { }

        public Review(Guid productId, int rating, string? text)
        {
            ProductId = productId;
            Rating = rating;
            Text = text;
        }
    }
}
