using MediatR;

namespace EShop.Application.Features.Commands.Brands.Create;

/// <summary>
///     Представляет команду для добавления бренда
/// </summary>
public record CreateBrandCommand(string Name) : IRequest<int>;
