using Microsoft.EntityFrameworkCore;
using DevShop.Api.Data;
using DevShop.Api.Models;

namespace DevShop.Api.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DevShopDbContext context) : base(context)
        {
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetLowStockAsync(long threshold)
        {
            return await _dbSet
                .Where(p => p.Quantity <= threshold)
                .ToListAsync();
        }
    }
} 