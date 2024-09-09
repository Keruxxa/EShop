using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.CQRS.Commands.Users;

/// <summary>
///     Представляет команду для удаления пользователя
/// </summary>
public record DeleteUserCommand(Guid Id) : IRequest<Result>;
