namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет сущность, связывающую <see cref="User"/> и <see cref="Entities.Role"/>
    /// </summary>
    public class UserRole
    {
        /// <summary>
        ///     Id роли, связанной с пользователем
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        ///     Роль, связанная с пользователем
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        ///     Id пользователя, связанного с ролью
        /// </summary>
        public Guid UserId { get; set; }
    }
}
