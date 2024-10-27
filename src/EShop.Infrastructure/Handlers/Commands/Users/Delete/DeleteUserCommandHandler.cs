using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.Exceptions;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Infrastructure.Handlers.Commands.Users.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteUserCommand"/>
/// </summary>
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Unit, Error>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<Result<Unit, Error>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        _userRepository.Delete(user);

        var isSaved = await _userRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
