﻿using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Commands.Brands
{
    /// <summary>
    ///     Представляет команду для удаления бренда
    /// </summary>
    public class DeleteBrandCommand : EntityBase<int>, IRequest<bool>
    {
    }
}
