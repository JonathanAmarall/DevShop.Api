namespace DevShop.Api.Models
{
    public class Customer : Entity
    {
        public Customer(string fullName, string phoneNumber, string address)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }

        public ICollection<Order> Orders { get; set; } = [];
    }
}
