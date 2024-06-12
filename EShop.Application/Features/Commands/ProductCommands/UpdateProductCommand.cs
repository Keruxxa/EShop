using MediatR;

namespace EShop.Application.Features.Commands.ProductCommands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; }

        public decimal Price { get; set; }

        public decimal? Rating { get; set; }

        public int CategoryId { get; }

        public int BrandId { get; }

        public int? CountryManufacturerId { get; }
    }
}
