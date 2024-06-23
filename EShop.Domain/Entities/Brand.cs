namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет бренд
    /// </summary>
    public class Brand : EntityBase<int>
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Товары, относящиеся к данному бренду
        /// </summary>
        public IReadOnlyCollection<Product> Products { get; }


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
}