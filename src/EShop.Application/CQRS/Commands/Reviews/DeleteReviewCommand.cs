using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Reviews;

public record DeleteReviewCommand(Guid ProductId, Guid UserId) : IRequest<Result<Unit, Error>>;
