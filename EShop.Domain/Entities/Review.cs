namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет отзыв
    /// </summary>
    public class Review : EntityBase<Guid>
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


        private Review() { }


        /// <param name="productId">Id товара</param>
        /// <param name="rating">Рейтинг</param>
        /// <param name="text">Текстовая часть отзыва</param>
        public Review(Guid productId, int rating, string? text)
        {
            ProductId = productId;
            Rating = rating;
            Text = text;
        }
    }
}
