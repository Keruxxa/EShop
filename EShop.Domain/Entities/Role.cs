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

        /// <summary>
        ///     Тип роли
        /// </summary>
        public RoleType RoleType { get; }


        private Role() { }

        public Role(string name, RoleType roleType)
        {
            Name = name;
            RoleType = roleType;
        }
    }
}
