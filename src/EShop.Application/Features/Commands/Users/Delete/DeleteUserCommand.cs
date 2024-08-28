using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.Features.Commands.Users.Delete;

/// <summary>
///     Представляет команду для удаления пользователя
/// </summary>
public record DeleteUserCommand(Guid Id) : IRequest<Result>;
