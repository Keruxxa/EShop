using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using static EShop.Application.Constants;

namespace EShop.Application.Features.Commands.Users.Delete
{
    /// <summary>
    ///     Представляет обработчик команды <see cref="DeleteUserCommand"/>
    /// </summary>
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly IEShopDbContext _dbContext;

        public DeleteUserCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

            if (user is null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            _dbContext.Users.Remove(user);

            var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            return saved
                ? Result.Success(saved)
                : Result.Failure<bool>(SERVER_SIDE_ERROR);
        }
    }
}
