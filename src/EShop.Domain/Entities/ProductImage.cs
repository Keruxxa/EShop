namespace EShop.Domain.Entities;

public class ProductImage
{
    public Guid Id { get; }

    public string Uri { get; private set; } = string.Empty;

    public bool IsMain { get; private set; }

    public byte Order { get; private set; }

    public Guid ProductId { get; }


    private ProductImage() { }

    public ProductImage(Guid productId, string fileName, bool isMain, byte order)
    {
        Id = Guid.CreateVersion7();
        ProductId = productId;
        IsMain = isMain;
        Order = order;

        Uri = $"{Id}_{fileName}";
    }
}
