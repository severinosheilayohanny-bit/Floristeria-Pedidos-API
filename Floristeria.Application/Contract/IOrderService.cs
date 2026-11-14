using Floristeria.Application.Core;
using Floristeria.Application.Dtos.Order;
using Floristeria.Domain.Entities;

namespace Floristeria.Application.Contract
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task<ServiceResult> CreateAsync(CreateOrderDto dto);

        Task<bool> UpdateAsync(int id, UpdateOrderDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
