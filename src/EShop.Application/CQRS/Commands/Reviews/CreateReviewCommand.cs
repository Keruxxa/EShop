using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.CQRS.Commands.Reviews;

/// <summary>
///     Представляет команду для создания отзыва
/// </summary>
/// /// <param name="UserId"> Id пользователя </param>
/// <param name="ProductId"> Id товара </param>
/// <param name="Rating"> Рейтинг </param>
/// <param name="Text"> Текстовая часть отзыва </param>
public record CreateReviewCommand(Guid UserId, Guid ProductId, int Rating, string? Text) : IRequest<Result<Review, Error>>;
