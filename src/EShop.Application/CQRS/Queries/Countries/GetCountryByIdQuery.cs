using CSharpFunctionalExtensions;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.CQRS.Queries.Countries;

/// <summary>
///     Представляет запрос для получения страны по ее Id
/// </summary>
public record GetCountryByIdQuery(int Id) : IRequest<Result<Country>>;
