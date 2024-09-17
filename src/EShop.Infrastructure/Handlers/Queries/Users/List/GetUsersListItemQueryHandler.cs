using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Dtos.User;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Users.List;

/// <summary>
///     Представялет обработчик запроса <see cref="GetUsersListItemQuery"/>
/// </summary>
public class GetUsersListItemQueryHandler : IRequestHandler<GetUsersListItemQuery, IEnumerable<UsersListItemDto>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public GetUsersListItemQueryHandler(IEShopDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }


    public async Task<IEnumerable<UsersListItemDto>> Handle(
        GetUsersListItemQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetListAsync(cancellationToken);

        return users.Select(user =>
        {
            return user.Adapt<UsersListItemDto>();
        });
    }
}
