using DevShop.Api.Models;

namespace DevShop.Api.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetByCodeAsync(string code);
        Task<Product?> GetByNameAsync(string name);
        Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Product>> GetLowStockAsync(long threshold);
    }
} 