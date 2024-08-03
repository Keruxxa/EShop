using EShop.Application.Interfaces.Security;
using BCrypt.Net;

namespace EShop.Infrastructure.Utilities.Security;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA256);
    }

    public bool Verify(string password, string hashPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
    }
}
