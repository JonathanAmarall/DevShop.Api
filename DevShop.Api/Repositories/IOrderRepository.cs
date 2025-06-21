using DevShop.Api.Models;

namespace DevShop.Api.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetWithDetailsAsync(string id);
        Task<IEnumerable<Order>> GetAllWithDetailsAsync();
        Task<IEnumerable<Order>> GetByCustomerIdAsync(string customerId);
        Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
        Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
} 