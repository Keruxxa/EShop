﻿using CSharpFunctionalExtensions;
using EShop.Application.Dtos.User;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Users.ById;

/// <summary>
///     Представялет обработчик запроса <see cref="GetUserByIdQuery"/>
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IEShopDbContext _dbContext;

    public GetUserByIdQueryHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        return user.Adapt<UserDto>();
    }
}
