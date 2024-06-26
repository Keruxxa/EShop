namespace EShop.Domain.Entities
{
    public class Category : EntityBase<int>
    {
        /// <summary>
        ///     Товары, относящиеся к данной категории
        /// </summary>
        private readonly List<CategoryProducts> _categotyProducts = [];

        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Товары, относящиеся к данной категории
        /// </summary>
        public IReadOnlyCollection<CategoryProducts> CategoryProducts => _categotyProducts.AsReadOnly();


        private Category() { }


        public Category(string name)
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
