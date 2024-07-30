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
        public string? FirstName { get; private set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        public string? LastName { get; private set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        public string? Phone { get; private set; }

        /// <summary>
        ///     Электронная почта
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        ///     Пароль
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        ///     Id роли
        /// </summary>
        public RoleType RoleId { get; private set; }

        /// <summary>
        ///     Роль
        /// </summary>
        public Role Role { get; private set; }


        private User() { }

        public User(
            Guid id,
            string? firstName,
            string? lastName,
            string? phone,
            string email,
            string password,
            RoleType roleId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Password = password;
            RoleId = roleId;
        }
    }
}
