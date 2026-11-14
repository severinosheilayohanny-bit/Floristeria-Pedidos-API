using Floristeria.Application.Core;
using Floristeria.Application.Dtos.Delivery;
using Floristeria.Domain.Entities;

namespace Floristeria.Application.Contract
{
    public interface IDeliveryService
    {
        Task<List<Delivery>> GetAllAsync();

        Task<Delivery?> GetByIdAsync(int id);

        Task<ServiceResult> CreateAsync(CreateDeliveryDto dto);

        Task<bool> UpdateAsync(int id, UpdateDeliveryDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
