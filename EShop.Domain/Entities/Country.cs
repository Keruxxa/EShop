namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет страну
    /// </summary>
    public class Country : EntityBase<int>
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; }


        private Country() { }

        public Country(string name)
        {
            Name = name;
        }
    }
}
