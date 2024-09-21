using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MapsterMapper;
using MediatR;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Users.SignUp;

/// <summary>
///     Представляет обработчик команды <see cref="SignUpUserCommandHandler"/>
/// </summary>
public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, Result<User>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public SignUpUserCommandHandler(
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


    public async Task<Result<User>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userService.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            throw new DuplicateEntityException(nameof(User));
        }

        if (!await _userService.IsPhoneUniqueAsync(request.Phone, cancellationToken))
        {
            throw new DuplicateEntityException(nameof(User));
        }

        request.SetHashPassword(_passwordHasher.Hash(request.Password));

        var user = _mapper.From(request).AdaptToType<User>();

        _userRepository.Create(user);

        var saved = await _userRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success(user)
            : Result.Failure<User>(SERVER_SIDE_ERROR);
    }
}
