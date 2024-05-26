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
        ///     Id сущности пользователь-роль
        /// </summary>
        public int UserRoleId { get; set; }

        /// <summary>
        ///     Сущность пользователь-роль
        /// </summary>
        public UserRole UserRole { get; set; }


        private User() { }
    }
}
