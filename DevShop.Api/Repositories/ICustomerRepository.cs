using DevShop.Api.Models;

namespace DevShop.Api.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetWithOrdersAsync(string id);
        Task<IEnumerable<Customer>> GetByPhoneNumberAsync(string phoneNumber);
        Task<IEnumerable<Customer>> GetByAddressAsync(string address);
    }
} 