using EShop.Domain.Enums;

namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет роль
    /// </summary>
    public class Role : EntityBase<RoleType>
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; }


        private Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}
