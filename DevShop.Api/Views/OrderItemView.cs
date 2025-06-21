namespace DevShop.Api.Views
{
    public class OrderItemView
    {
        public string Id { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreateOnUtc { get; set; }
        public DateTime? UpdateOnUtc { get; set; }
        public ProductView? Product { get; set; }
    }

    public class CreateOrderItemView
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class UpdateOrderItemView
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
} 