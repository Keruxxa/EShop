using CSharpFunctionalExtensions;
using EShop.Application.Dtos.User;
using MediatR;

namespace EShop.Application.Features.Queries.Users.ById;

/// <summary>
///     Представляет запрос для получения пользователя по его Id
/// </summary>
public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;
