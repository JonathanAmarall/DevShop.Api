namespace DevShop.Api.Views
{
    public class ProductView
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public long Quantity { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime CreateOnUtc { get; set; }
        public DateTime? UpdateOnUtc { get; set; }
    }

    public class CreateProductView
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public long Quantity { get; set; }
        public string Code { get; set; } = string.Empty;
    }

    public class UpdateProductView
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public long Quantity { get; set; }
        public string Code { get; set; } = string.Empty;
    }
} 