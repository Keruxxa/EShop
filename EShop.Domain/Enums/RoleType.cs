namespace EShop.Domain.Enums
{
    /// <summary>
    ///     Представляет перечисление ролей
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        ///     Администратор
        /// </summary>
        Administrator = 1,

        /// <summary>
        ///     Менеджер
        /// </summary>
        Manager,

        /// <summary>
        ///     Зарегистрированный пользователь
        /// </summary>
        RegisteredUser,

        /// <summary>
        ///     Незарегистрированный пользователь
        /// </summary>
        UnregisteredUser
    }
}
