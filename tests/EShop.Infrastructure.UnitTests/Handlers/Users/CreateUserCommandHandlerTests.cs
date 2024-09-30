using EShop.Application.CQRS.Commands.Users;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using EShop.Domain.Enums;
using EShop.Infrastructure.Handlers.Commands.Users.Create;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using static EShop.Application.Constants;

namespace EShop.Application.UnitTests.Handlers.Users;

public class CreateUserCommandHandlerTests
{
    private readonly CreateUserCommandHandler _handler;
    private readonly Mock<IEShopDbContext> _dbContextMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ITypeAdapterBuilder<CreateUserCommand>> _typeAdapterBuilderMock = new();

    private readonly Guid id = Guid.NewGuid();
    private readonly string firstName = "First";
    private readonly string lastName = "Last";
    private readonly string phone = "89999999999";
    private readonly string email = "email.@gmail.com";
    private readonly string password = "password";
    private readonly string hashPassword = "hash_password";
    private readonly RoleType roleType = RoleType.Administrator;

    private readonly int entityCountSaved = 1;

    public CreateUserCommandHandlerTests()
    {
        _handler = new CreateUserCommandHandler(
            _userRepositoryMock.Object,
            _dbContextMock.Object,
            _passwordHasherMock.Object,
            _userServiceMock.Object,
            _mapperMock.Object);
    }


    [Fact]
    public async Task Handler_ShouldReturnFailureResult_WhenEmailIsNotUnique()
    {
        //Arrange
        var command = GetCreateUserCommand();

        var error = new Error(new DuplicateEntityError(nameof(User), USER_EMAIL_IS_NOT_UNIQUE), ErrorType.Duplicate);

        _userServiceMock
            .Setup(x => x.IsEmailUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        Assert.IsType<DuplicateEntityError>(result.Error.EntityError);
        Assert.Equal(error.ErrorType, result.Error.ErrorType);
    }

    [Fact]
    public async Task Handler_ShouldReturnFailureResult_WhenPhoneIsNotUnique()
    {
        //Arrange
        var command = GetCreateUserCommand();

        var error = new Error(new DuplicateEntityError(nameof(User), USER_PHONE_IS_NOT_UNIQUE), ErrorType.Duplicate);

        _userServiceMock
            .Setup(x => x.IsEmailUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _userServiceMock
            .Setup(x => x.IsPhoneUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        Assert.IsType<DuplicateEntityError>(result.Error.EntityError);
        Assert.Equal(error.ErrorType, result.Error.ErrorType);
    }

    [Fact]
    public async Task Handler_ShouldReturnUserId_WhenUserIsAddedAndSaved()
    {
        //Arrange
        var command = GetCreateUserCommand();

        var user = GetUser();

        _userServiceMock
            .Setup(x => x.IsEmailUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _userServiceMock
            .Setup(x => x.IsPhoneUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _passwordHasherMock
            .Setup(x => x.Hash(command.Password))
            .Returns(hashPassword);

        _mapperMock
            .Setup(x => x.From(command))
            .Returns(_typeAdapterBuilderMock.Object);

        _typeAdapterBuilderMock
            .Setup(x => x.AdaptToType<User>())
            .Returns(user);

        _userRepositoryMock
            .Setup(x => x.Create(user))
            .Returns(id);

        _userRepositoryMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(entityCountSaved);

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        Assert.Equal(id, result.Value);
    }

    [Fact]
    public async Task Handler_ShouldReturnFailureResult_WhenUserAddedAndNotSaved()
    {
        //Arrange
        var command = GetCreateUserCommand();

        var user = GetUser();

        var error = new Error(new ServerEntityError(), ErrorType.ServerError);

        _userServiceMock
            .Setup(x => x.IsEmailUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _userServiceMock
            .Setup(x => x.IsPhoneUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _passwordHasherMock
            .Setup(x => x.Hash(command.Password))
            .Returns(hashPassword);

        _mapperMock
            .Setup(x => x.From(command))
            .Returns(_typeAdapterBuilderMock.Object);

        _typeAdapterBuilderMock
            .Setup(x => x.AdaptTo(user));

        _dbContextMock
            .Setup(x => x.Users)
            .Returns(new Mock<DbSet<User>>().Object);

        _dbContextMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        Assert.IsType<ServerEntityError>(result.Error.EntityError);
        Assert.Equal(error.ErrorType, result.Error.ErrorType);
    }

    private CreateUserCommand GetCreateUserCommand()
    {
        return new(firstName, lastName, phone, email, password);
    }

    private User GetUser()
    {
        return new User(id, firstName, lastName, phone, email, password, roleType);
    }
}
