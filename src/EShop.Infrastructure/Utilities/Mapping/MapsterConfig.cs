using EShop.Application.Dtos.User;
using EShop.Domain.Entities;
using EShop.Domain.Enums;
using Mapster;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Dtos.Basket;
using EShop.Application.Dtos.Product;
using EShop.Application.Dtos.Orders;
using EShop.Application.Dtos.Favorite;

namespace EShop.Infrastructure.Utilities.Mapping;

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

        TypeAdapterConfig<Basket, BasketDto>
            .NewConfig()
            .ConstructUsing(src => new BasketDto(
                src.Id,
                src.TotalPrice,
                src.BasketItems.Select(basketItem => new ProductInBasketDto(
                    basketItem.Product.Id,
                    basketItem.Product.Name,
                    basketItem.Product.Price,
                    basketItem.Count)).ToList()));

        TypeAdapterConfig<Order, OrderDto>
            .NewConfig()
            .ConstructUsing(src => new OrderDto(
                src.OrderingDate,
                src.TotalPrice,
                src.OrderItems.Select(x => new ProductInOrderDto(
                    x.Product.Id,
                    x.Product.Name,
                    x.Product.Price)).ToList()));

        TypeAdapterConfig<Favorite, FavoriteDto>
            .NewConfig()
            .ConstructUsing(src => new FavoriteDto(
                src.Id,
                src.FavoriteProducts.Select(favoriteProduct => new ProductInFavoriteDto(
                    favoriteProduct.Product.Id,
                    favoriteProduct.Product.Name,
                    favoriteProduct.Product.Price)).ToList()));
    }
}
