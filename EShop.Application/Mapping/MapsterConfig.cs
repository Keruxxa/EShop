using EShop.Application.Dtos.User;
using EShop.Application.Features.Commands.Users.Create;
using EShop.Application.Features.Commands.Users.SignIn;
using EShop.Domain.Entities;
using EShop.Domain.Enums;
using Mapster;

namespace EShop.Application.Mapping
{
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
                .MapWith(src => new User(
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

            TypeAdapterConfig<SignUpUserCommand, User>
                .NewConfig()
                .MapWith(src => new User(
                    Guid.NewGuid(),
                    src.FirstName,
                    src.LastName,
                    src.Phone,
                    src.Email,
                    src.HashPassword,
                    RoleType.RegisteredUser
                ));
        }
    }
}
