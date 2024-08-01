using EShop.Application.Dtos.User;
using EShop.Domain.Enums;
using MediatR;

namespace EShop.Application.Features.Queries.Users.List
{
    /// <summary>
    ///     Представляет запрос для получения списка пользователей с ролью <see cref="RoleType.Manager"/>
    /// </summary>
    public record GetUsersListItemQuery : IRequest<IEnumerable<UsersListItemDto>>;
}
