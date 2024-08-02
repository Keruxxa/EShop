using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Security;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

using static EShop.Application.Constants;

namespace EShop.Application.Features.Commands.Users.SignIn
{
    /// <summary>
    ///     Представляет обработчик команды <see cref="SignUpUserCommandHandler"/>
    /// </summary>
    public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, Result<User>>
    {
        private readonly IEShopDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public SignUpUserCommandHandler(IEShopDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }


        public async Task<Result<User>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
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
                ? Result.Success(user)
                : Result.Failure<User>(SERVER_SIDE_ERROR);
        }
    }
}
