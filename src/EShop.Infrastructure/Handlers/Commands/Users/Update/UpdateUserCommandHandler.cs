using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Users.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateUserCommand"/>
/// </summary>
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IEShopDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }


    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        user.UpdateMainInfo(request.FirstName, request.LastName);

        _userRepository.Update(user);

        var saved = await _userRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure(SERVER_SIDE_ERROR);
    }
}

