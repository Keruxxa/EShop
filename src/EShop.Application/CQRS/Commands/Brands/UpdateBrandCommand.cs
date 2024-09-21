using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.CQRS.Commands.Brands;

public record UpdateBrandCommand(int Id, string Name) : IRequest<Result>;
