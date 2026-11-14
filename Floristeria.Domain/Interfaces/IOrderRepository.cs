using Floristeria.Domain.Entities;

namespace Floristeria.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task<bool> CustomerExistsAsync(int customerId);

        Task<Dictionary<int, Product>> GetProductsByIdsAsync(
            List<int> productIds);

        Task<Order> AddAsync(Order order);

        Task<bool> UpdateAsync(Order order);

        Task<bool> DeleteAsync(int id);
    }
}
