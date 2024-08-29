using CSharpFunctionalExtensions;
using EShop.Application.Interfaces.Services;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using MapsterMapper;
using MediatR;
using EShop.Application.Interfaces.Security;
using static EShop.Application.Constants;
using EShop.Application.CQRS.Commands.Users;

namespace EShop.Infrastructure.Handlers.Commands.Users.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateUserCommandHandler"/>
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(
        IEShopDbContext dbContext,
        IPasswordHasher passwordHasher,
        IUserService userService,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _userService = userService;
        _mapper = mapper;
    }


    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userService.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            return Result.Failure<Guid>(USER_EMAIL_IS_NOT_UNIQUE);
        }

        if (!await _userService.IsPhoneUniqueAsync(request.Phone, cancellationToken))
        {
            return Result.Failure<Guid>(USER_PHONE_IS_NOT_UNIQUE);
        }

        request.HashPassword = _passwordHasher.Hash(request.Password);

        var user = _mapper.From(request).AdaptToType<User>();

        _dbContext.Users.Add(user);

        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success(user.Id)
            : Result.Failure<Guid>(SERVER_SIDE_ERROR);
    }
}
