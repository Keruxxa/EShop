using EShop.Application.Dtos.User;
using EShop.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Enums;

namespace EShop.Application.Features.Queries.Users.List;

/// <summary>
///     Представялет обработчик запроса <see cref="GetUsersListItemQuery"/>
/// </summary>
public class GetUsersListItemQueryHandler : IRequestHandler<GetUsersListItemQuery, IEnumerable<UsersListItemDto>>
{
    private readonly IEShopDbContext _dbContext;

    public GetUsersListItemQueryHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<UsersListItemDto>> Handle(
        GetUsersListItemQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users
            .Where(user => user.RoleId == RoleType.Manager)
            .ToListAsync(cancellationToken);

        return users.Select(user =>
        {
            return user.Adapt<UsersListItemDto>();
        });
    }
}
