using Microsoft.EntityFrameworkCore;
using DevShop.Api.Data;
using DevShop.Api.Models;

namespace DevShop.Api.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DevShopDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetWithOrdersAsync(string id)
        {
            return await _dbSet
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _dbSet
                .Where(c => c.PhoneNumber.Contains(phoneNumber))
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetByAddressAsync(string address)
        {
            return await _dbSet
                .Where(c => c.Address.Contains(address))
                .ToListAsync();
        }
    }
} 