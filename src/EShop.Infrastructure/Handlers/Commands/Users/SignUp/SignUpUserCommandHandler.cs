using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.Dtos.User;
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

namespace EShop.Infrastructure.Handlers.Commands.Users.SignUp;

/// <summary>
///     Представляет обработчик команды <see cref="SignUpUserCommandHandler"/>
/// </summary>
public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, Result<SignUpUserResponseDto, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IJwtTokenService _jwtTokenService;

    public SignUpUserCommandHandler(
        IUserRepository userRepository,
        IEShopDbContext dbContext,
        IPasswordHasher passwordHasher,
        IUserService userService,
        IMapper mapper,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _userService = userService;
        _mapper = mapper;
        _jwtTokenService = jwtTokenService;
    }


    public async Task<Result<SignUpUserResponseDto, Error>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userService.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            return Result.Failure<SignUpUserResponseDto, Error>(new Error(
                new DuplicateEntityError(nameof(User), USER_EMAIL_IS_NOT_UNIQUE), ErrorType.Duplicate));
        }

        if (request.Phone is not null)
        {
            if (!await _userService.IsPhoneUniqueAsync(request.Phone, cancellationToken))
            {
                return Result.Failure<SignUpUserResponseDto, Error>(new Error(
                    new DuplicateEntityError(nameof(User), USER_PHONE_IS_NOT_UNIQUE), ErrorType.Duplicate));
            }
        }

        request.SetHashPassword(_passwordHasher.Hash(request.Password));

        var user = _mapper.From(request).AdaptToType<User>();

        _userRepository.Add(user);

        var isSaved = await _userRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!isSaved)
        {
            return Result.Failure<SignUpUserResponseDto, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
        }

        var token = _jwtTokenService.Generate(user);

        return isSaved
            ? Result.Success<SignUpUserResponseDto, Error>(new SignUpUserResponseDto(user.Id, token))
            : Result.Failure<SignUpUserResponseDto, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
