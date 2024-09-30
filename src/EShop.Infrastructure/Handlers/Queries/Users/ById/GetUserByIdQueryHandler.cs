using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Dtos.User;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Issues.Errors;
using EShop.Domain.Entities;
using Mapster;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Users.ById;

/// <summary>
///     Представялет обработчик запроса <see cref="GetUserByIdQuery"/>
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IEShopDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }


    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(new NotFoundEntityError(nameof(User), request.Id).Message);
        }

        return user.Adapt<UserDto>();
    }
}
