namespace DevShop.Api.Models
{
    public class Order : Entity
    {
        public Order(string customerId, decimal totalAmount)
        {
            CustomerId = customerId;
            TotalAmount = totalAmount;
            Status = OrderStatus.Pending;
            OrderDateOnUtc = DateTime.UtcNow;
        }

        public void AddItem(OrderItem item)
        {
            Items ??= [];

            Items.Add(item);
        }

        public string CustomerId { get; private set; }
        public DateTime OrderDateOnUtc { get; private set; }
        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; }

        public Customer Customer { get; set; } = null!;
        public ICollection<OrderItem> Items { get; private set; } = [];
    }
}
