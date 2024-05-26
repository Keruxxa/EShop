namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет отзыв
    /// </summary>
    public class Review : EntityBase<int>
    {
        /// <summary>
        ///     Текстовая часть отзыва
        /// </summary>
        public string? Text { get; }

        /// <summary>
        ///     Рейтинг
        /// </summary>
        public int Rating { get; }

        /// <summary>
        ///     Id товара, связанного с отзывом
        /// </summary>
        public Guid ProductId { get; }

        /// <summary>
        ///     Товар, связанный с отзывом
        /// </summary>
        public Product Product { get; }


        private Review() { }

        public Review(Guid productId, string? text, int rating)
        {
            ProductId = productId;
            Rating = rating;
            Text = text;
        }
    }
}
