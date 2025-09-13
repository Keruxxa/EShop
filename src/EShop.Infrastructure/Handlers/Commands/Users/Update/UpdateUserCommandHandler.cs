using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;
using EShop.Infrastructure.Extensions;

namespace EShop.Infrastructure.Handlers.Commands.Users.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateUserCommand"/>
/// </summary>
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Unit, Error>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<Result<Unit, Error>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Product), request.Id), ErrorType.NotFound));
        }


        user.UpdateMainInfo(request.FirstName?.ToNullIfEmpty(), request.LastName?.ToNullIfEmpty());

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success<Unit, Error>(Unit.Value);
    }
}

