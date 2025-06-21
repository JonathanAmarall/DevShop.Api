namespace DevShop.Api.Models
{
    public class OrderItem : Entity
    {
        public OrderItem(string orderId, string productId, int quantity, decimal unitPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public string OrderId { get; private set; }
        public string ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public Product Product { get; set; } = null!;
        public Order Order { get; set; } = null!;
    }
}
