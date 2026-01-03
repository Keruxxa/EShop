using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Dtos.User;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using EShop.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Queries.Users.ById;

/// <summary>
///     Представялет обработчик запроса <see cref="GetUserByIdQuery"/>
/// </summary>
public class GetUserByIdQueryHandler(EShopDbContext dbContext) : IRequestHandler<GetUserByIdQuery, Result<UserDto, Error>>
{
    public async Task<Result<UserDto, Error>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(u => u.Id == request.Id)
            .Select(u => new UserDto(u.FirstName, u.LastName, u.Phone, u.Email, u.Role!.Name))
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto, Error>(new Error(new NotFoundEntityError(nameof(User), request.Id), ErrorType.NotFound));
        }

        return user;
    }
}
