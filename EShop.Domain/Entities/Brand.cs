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


        private Brand() { }

        public Brand(string name)
        {
            Name = name;
        }
    }
}