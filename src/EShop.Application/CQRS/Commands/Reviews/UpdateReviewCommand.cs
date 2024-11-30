using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Reviews;

/// <summary>
///     Представляет команду для обновления отзыва
/// </summary>
/// <param name="Id"> Id отзыва </param>
/// <param name="Rating"> Рейтинг </param>
/// <param name="Text"> Текстовая часть отзыва </param>
public record UpdateReviewCommand(Guid ProductId, Guid UserId, int Rating, string? Text) : IRequest<Result<Unit, Error>>;
