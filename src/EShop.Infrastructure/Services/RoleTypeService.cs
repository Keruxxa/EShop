using EShop.Application.Interfaces.Services;
using EShop.Domain.Enums;

namespace EShop.Infrastructure.Services
{
    public class RoleTypeService : IRoleTypeService
    {
        public string GetRoleTypeName(RoleType roleType)
        {
            return roleType switch
            {
                RoleType.Administrator => RoleType.Administrator.ToString(),
                RoleType.Manager => RoleType.Manager.ToString(),
                RoleType.RegisteredUser => RoleType.RegisteredUser.ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(roleType))
            };
        }
    }
}
