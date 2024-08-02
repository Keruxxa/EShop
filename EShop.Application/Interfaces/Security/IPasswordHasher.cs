namespace EShop.Application.Interfaces.Security
{
    public interface IPasswordHasher
    {
        /// <summary>
        ///     Генерирует хеш-пароль на основе заданного пароля
        /// </summary>
        string Hash(string password);

        /// <summary>
        ///     Проверяет, соответствует ли введенный пароль хеш-паролю
        /// </summary>
        bool Verify(string password, string hashPassword);
    }
}
