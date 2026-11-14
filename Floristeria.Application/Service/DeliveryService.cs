using Floristeria.Application.Contract;
using Floristeria.Application.Core;
using Floristeria.Application.Dtos.Delivery;
using Floristeria.Domain.Entities;
using Floristeria.Domain.Interfaces;

namespace Floristeria.Application.Service
{
    public class DeliveryService : BaseService, IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<List<Delivery>> GetAllAsync()
        {
            return await _deliveryRepository.GetAllAsync();
        }

        public async Task<Delivery?> GetByIdAsync(int id)
        {
            return await _deliveryRepository.GetByIdAsync(id);
        }

        public async Task<ServiceResult> CreateAsync(CreateDeliveryDto dto)
        {
            var orderExists =
                await _deliveryRepository.OrderExistsAsync(dto.OrderId);

            if (!orderExists)
            {
                return CreateErrorResult("El pedido no existe.");
            }

            var deliveryExists =
                await _deliveryRepository.DeliveryExistsForOrderAsync(
                    dto.OrderId);

            if (deliveryExists)
            {
                return CreateErrorResult(
                    "Este pedido ya tiene una entrega registrada.");
            }

            if (string.IsNullOrWhiteSpace(dto.Address))
            {
                return CreateErrorResult(
                    "La dirección de entrega es obligatoria.");
            }

            if (dto.ScheduledDate <= DateTime.UtcNow)
            {
                return CreateErrorResult(
                    "La fecha programada debe ser futura.");
            }

            var delivery = new Delivery
            {
                OrderId = dto.OrderId,
                Address = dto.Address.Trim(),
                ScheduledDate = dto.ScheduledDate,
                Status = "Pending",
                DeliveredDate = null
            };

            var createdDelivery =
                await _deliveryRepository.AddAsync(delivery);

            return new ServiceResult
            {
                Success = true,
                Message = "Entrega creada correctamente.",
                Data = createdDelivery
            };
        }

        public async Task<bool> UpdateAsync(
            int id, UpdateDeliveryDto dto)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);

            if (delivery == null)
            {
                return false;
            }

            delivery.Status = dto.Status;

            if (dto.Status == "Delivered")
            {
                delivery.DeliveredDate =
                    delivery.DeliveredDate ?? DateTime.UtcNow;
            }
            else
            {
                delivery.DeliveredDate = null;
            }

            return await _deliveryRepository.UpdateAsync(delivery);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _deliveryRepository.DeleteAsync(id);
        }
    }
}
