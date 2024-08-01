using EShop.Application.Dtos.User;
using EShop.Application.Features.Commands.Users.Create;
using EShop.Domain.Entities;
using EShop.Domain.Enums;
using Mapster;

namespace EShop.Application.Mapping
{
    public static class MapsterConfig
    {
        public static void ConfigureMapping()
        {
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

            TypeAdapterConfig<CreateUserDto, CreateUserCommand>
                .NewConfig()
                .ConstructUsing(src => new CreateUserCommand(
                    src.FirstName,
                    src.LastName,
                    src.Phone,
                    src.Email,
                    src.Password));

            TypeAdapterConfig<User, UserDto>
                .NewConfig()
                .ConstructUsing(src => new UserDto(
                    src.FirstName,
                    src.LastName,
                    src.Phone,
                    src.Email,
                    src.Role.Name));
        }
    }
}
