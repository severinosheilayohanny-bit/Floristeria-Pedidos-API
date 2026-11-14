using Floristeria.API.DTOs;
using Floristeria.Domain.Entities;
using Floristeria.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Floristeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveriesController(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        // GET: api/deliveries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
            var deliveries = await _deliveryRepository.GetAllAsync();

            return Ok(deliveries);
        }

        // GET: api/deliveries/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Delivery>> GetDelivery(int id)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);

            if (delivery == null)
                return NotFound();

            return Ok(delivery);
        }

        // POST: api/deliveries
        [HttpPost]
        public async Task<ActionResult<Delivery>> CreateDelivery(
            CreateDeliveryDto dto)
        {
            var orderExists =
                await _deliveryRepository.OrderExistsAsync(dto.OrderId);

            if (!orderExists)
                return BadRequest("El pedido no existe.");

            var deliveryExists =
                await _deliveryRepository.DeliveryExistsForOrderAsync(
                    dto.OrderId);

            if (deliveryExists)
                return BadRequest(
                    "Este pedido ya tiene una entrega registrada.");

            if (string.IsNullOrWhiteSpace(dto.Address))
                return BadRequest(
                    "La dirección de entrega es obligatoria.");

            if (dto.ScheduledDate <= DateTime.UtcNow)
                return BadRequest(
                    "La fecha programada debe ser futura.");

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

            return CreatedAtAction(
                nameof(GetDelivery),
                new { id = createdDelivery.Id },
                new
                {
                    createdDelivery.Id,
                    createdDelivery.OrderId,
                    createdDelivery.Address,
                    createdDelivery.ScheduledDate,
                    createdDelivery.Status,
                    createdDelivery.DeliveredDate
                });
        }

        // PUT: api/deliveries/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDelivery(
            int id, UpdateDeliveryDto dto)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);

            if (delivery == null)
                return NotFound();

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

            var updated =
                await _deliveryRepository.UpdateAsync(delivery);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/deliveries/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var deleted = await _deliveryRepository.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
