﻿using MediatR;

namespace EShop.Application.CQRS.Commands.Brands;

/// <summary>
///     Представляет команду для добавления бренда
/// </summary>
public record CreateBrandCommand(string Name) : IRequest<int>;
