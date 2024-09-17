using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Users.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteUserCommand"/>
/// </summary>
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        _userRepository.Delete(user);

        var saved = await _userRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure(SERVER_SIDE_ERROR);
    }
}
