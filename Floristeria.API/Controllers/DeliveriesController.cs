using Floristeria.Application.Contract;
using Floristeria.Application.Dtos.Delivery;
using Floristeria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Floristeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveriesController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        // GET: api/deliveries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
            var deliveries = await _deliveryService.GetAllAsync();

            return Ok(deliveries);
        }

        // GET: api/deliveries/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Delivery>> GetDelivery(int id)
        {
            var delivery = await _deliveryService.GetByIdAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            return Ok(delivery);
        }

        // POST: api/deliveries
        [HttpPost]
        public async Task<ActionResult<Delivery>> CreateDelivery(
            CreateDeliveryDto dto)
        {
            var result = await _deliveryService.CreateAsync(dto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            if (result.Data is not Delivery createdDelivery)
            {
                return StatusCode(
                    500,
                    "No se pudo recuperar la entrega creada.");
            }

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
            var updated = await _deliveryService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/deliveries/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var deleted = await _deliveryService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
