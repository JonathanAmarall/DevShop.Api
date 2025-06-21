using DevShop.Api.Models;

namespace DevShop.Api.Views
{
    public class OrderView
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public DateTime OrderDateOnUtc { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreateOnUtc { get; set; }
        public DateTime? UpdateOnUtc { get; set; }
        public CustomerView? Customer { get; set; }
        public List<OrderItemView> Items { get; set; } = [];
    }

    public class CreateOrderView
    {
        public string CustomerId { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public List<CreateOrderItemView> Items { get; set; } = [];
    }

    public class UpdateOrderView
    {
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
    }
} 