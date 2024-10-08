﻿using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MapsterMapper;
using MediatR;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Users.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateUserCommandHandler"/>
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IEShopDbContext dbContext,
        IPasswordHasher passwordHasher,
        IUserService userService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _userService = userService;
        _mapper = mapper;
    }


    public async Task<Result<Guid, Error>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userService.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            return Result.Failure<Guid, Error>(new Error(
                new DuplicateEntityError(nameof(User), USER_EMAIL_IS_NOT_UNIQUE), ErrorType.Duplicate));
        }

        if (!await _userService.IsPhoneUniqueAsync(request.Phone, cancellationToken))
        {
            return Result.Failure<Guid, Error>(new Error(
                new DuplicateEntityError(nameof(User), USER_PHONE_IS_NOT_UNIQUE), ErrorType.Duplicate));
        }

        request.SetHashPassword(_passwordHasher.Hash(request.Password));

        var user = _mapper.From(request).AdaptToType<User>();

        _userRepository.Create(user);

        var isSaved = await _userRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Guid, Error>(user.Id)
            : Result.Failure<Guid, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
