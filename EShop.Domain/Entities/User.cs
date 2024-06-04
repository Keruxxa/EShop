using EShop.Domain.Enums;

namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет пользователя
    /// </summary>
    public class User : EntityBase<Guid>
    {
        /// <summary>
        ///     Имя
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        ///     Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Id роли
        /// </summary>
        public RoleType RoleId { get; set; }

        /// <summary>
        ///     Роль
        /// </summary>
        public Role Role { get; set; }


        private User() { }
    }
}
