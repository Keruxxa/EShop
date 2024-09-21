using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Brands;

public record UpdateBrandCommand(int Id, string Name) : IRequest<Result<Unit, Error>>;
