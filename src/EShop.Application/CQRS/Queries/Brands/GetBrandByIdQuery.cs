﻿using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.CQRS.Queries.Brands;

/// <summary>
///     Представляет запрос для получения бренда по его Id
/// </summary>
public record GetBrandByIdQuery(int Id) : IRequest<Result<Brand, Error>>;
