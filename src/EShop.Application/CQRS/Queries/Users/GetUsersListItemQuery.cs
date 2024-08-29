using EShop.Application.Dtos.User;
using EShop.Domain.Enums;
using MediatR;

namespace EShop.Application.CQRS.Queries.Users;

/// <summary>
///     Представляет запрос для получения списка пользователей с ролью <see cref="RoleType.Manager"/>
/// </summary>
public record GetUsersListItemQuery : IRequest<IEnumerable<UsersListItemDto>>;
