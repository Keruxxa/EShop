using EShop.Domain.Enums;

namespace EShop.Application.Interfaces.Services;

public interface IRoleTypeService
{
    string GetRoleTypeName(RoleType roleType);
}
