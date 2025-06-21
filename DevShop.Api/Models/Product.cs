namespace DevShop.Api.Models
{
    public class Product : Entity
    {
        public Product(string name, string description, decimal price, long quantity, string code)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            Code = code;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public long Quantity { get; private set; }
        public string Code { get; private set; }
    }
}
