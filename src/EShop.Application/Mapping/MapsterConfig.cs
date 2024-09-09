using EShop.Application.Dtos.User;
using EShop.Domain.Entities;
using EShop.Domain.Enums;
using Mapster;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.CQRS.Queries.Users;

namespace EShop.Application.Mapping;

public static class MapsterConfig
{
    public static void ConfigureMapping()
    {
        TypeAdapterConfig<CreateUserDto, CreateUserCommand>
            .NewConfig()
            .ConstructUsing(src => new CreateUserCommand(
                src.FirstName,
                src.LastName,
                src.Phone,
                src.Email,
                src.Password));

        TypeAdapterConfig<CreateUserCommand, User>
            .NewConfig()
            .Ignore(dest => dest.Password)
            .ConstructUsing(src => new User(
                Guid.NewGuid(),
                src.FirstName,
                src.LastName,
                src.Phone,
                src.Email,
                src.HashPassword,
                RoleType.Manager
            ));

        TypeAdapterConfig<User, UserDto>
            .NewConfig()
            .ConstructUsing(src => new UserDto(
                src.FirstName,
                src.LastName,
                src.Phone,
                src.Email,
                src.Role.Name));

        TypeAdapterConfig<SignUpUserDto, SignUpUserCommand>
            .NewConfig()
            .ConstructUsing(src => new SignUpUserCommand(
                src.FirstName,
                src.LastName,
                src.Phone,
                src.Email,
                src.Password));

        TypeAdapterConfig<SignUpUserCommand, User>.NewConfig().MapToConstructor(true);

        TypeAdapterConfig<SignUpUserCommand, User>
            .NewConfig()
            .Ignore(dest => dest.Password)
            .ConstructUsing(src => new User(
                Guid.NewGuid(),
                src.FirstName,
                src.LastName,
                src.Phone,
                src.Email,
                src.HashPassword,
                RoleType.RegisteredUser
            ));

        TypeAdapterConfig<SignInUserDto, SignInUserQuery>
            .NewConfig()
            .ConstructUsing(src => new SignInUserQuery(
                src.Email,
                src.Password));
    }
}
