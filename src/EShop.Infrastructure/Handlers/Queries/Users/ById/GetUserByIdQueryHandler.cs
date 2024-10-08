using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Dtos.User;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using Mapster;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Users.ById;

/// <summary>
///     Представялет обработчик запроса <see cref="GetUserByIdQuery"/>
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto, Error>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<Result<UserDto, Error>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto, Error>(new Error(new NotFoundEntityError(nameof(User), request.Id), ErrorType.NotFound));
        }

        return user.Adapt<UserDto>();
    }
}
