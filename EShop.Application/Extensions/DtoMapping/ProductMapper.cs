using EShop.Application.Dtos.Product;
using EShop.Domain.Entities;

namespace EShop.Application.Extensions.DtoMapping
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto(product.Name, product.Description, product.ReleaseDate, product.Price,
                product.Category.Name, product.CountryManufacturer.Name);
        }
    }
}
