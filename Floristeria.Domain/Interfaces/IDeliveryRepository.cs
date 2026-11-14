using Floristeria.Domain.Entities;

namespace Floristeria.Domain.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<List<Delivery>> GetAllAsync();

        Task<Delivery?> GetByIdAsync(int id);

        Task<bool> OrderExistsAsync(int orderId);

        Task<bool> DeliveryExistsForOrderAsync(int orderId);

        Task<Delivery> AddAsync(Delivery delivery);

        Task<bool> UpdateAsync(Delivery delivery);

        Task<bool> DeleteAsync(int id);
    }
}
