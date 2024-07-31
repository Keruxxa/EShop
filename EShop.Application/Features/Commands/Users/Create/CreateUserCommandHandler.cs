﻿using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Security;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

using static EShop.Domain.Constants;

namespace EShop.Application.Features.Commands.Users.Create
{
    /// <summary>
    ///     Представляет обработчик команды <see cref="CreateUserCommandHandler"/>
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly IEShopDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(IEShopDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IEShopDbContext));
            _passwordHasher = passwordHasher;
        }


        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Email is not null)
            {
                var emailIsUsed = await _dbContext.Users
                    .AnyAsync(user => user.Email.Equals(request.Email), cancellationToken);

                if (emailIsUsed)
                {
                    throw new DuplicateEntityException(nameof(User));
                }
            }

            if (request.Phone is not null)
            {
                var phoneIsUsed = await _dbContext.Users
                    .AnyAsync(user => user.Phone.Equals(request.Phone), cancellationToken);

                if (phoneIsUsed)
                {
                    throw new DuplicateEntityException(nameof(User));
                }
            }

            request.HashPassword = _passwordHasher.Hash(request.Password);

            var user = request.Adapt<User>();

            await _dbContext.Users.AddAsync(user, cancellationToken);

            var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            return saved
                ? Result.Success(user.Id)
                : Result.Failure<Guid>(SERVER_SIDE_ERROR);
        }
    }
}
