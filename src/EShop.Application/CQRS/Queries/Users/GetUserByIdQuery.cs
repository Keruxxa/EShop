﻿using CSharpFunctionalExtensions;
using EShop.Application.Dtos.User;
using MediatR;

namespace EShop.Application.CQRS.Queries.Users;

/// <summary>
///     Представляет запрос для получения пользователя по его Id
/// </summary>
public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;
