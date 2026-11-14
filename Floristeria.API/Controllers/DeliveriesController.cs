using Floristeria.API.Context;
using Floristeria.API.DTOs;
using Floristeria.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Floristeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeliveriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/deliveries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
            var deliveries = await _context.Deliveries
                .Include(d => d.Order)
                .AsNoTracking()
                .ToListAsync();

            return Ok(deliveries);
        }

        // GET: api/deliveries/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Delivery>> GetDelivery(int id)
        {
            var delivery = await _context.Deliveries
                .Include(d => d.Order)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (delivery == null)
                return NotFound();

            return Ok(delivery);
        }

        // POST: api/deliveries
        [HttpPost]
        public async Task<ActionResult<Delivery>> CreateDelivery(
            CreateDeliveryDto dto)
        {
            var orderExists = await _context.Orders
                .AnyAsync(o => o.Id == dto.OrderId);

            if (!orderExists)
                return BadRequest("El pedido no existe.");

            var deliveryExists = await _context.Deliveries
                .AnyAsync(d => d.OrderId == dto.OrderId);

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

            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDelivery),
                new { id = delivery.Id },
                new
                {
                    delivery.Id,
                    delivery.OrderId,
                    delivery.Address,
                    delivery.ScheduledDate,
                    delivery.Status,
                    delivery.DeliveredDate
                });
        }

        // PUT: api/deliveries/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDelivery(
            int id, UpdateDeliveryDto dto)
        {
            var delivery = await _context.Deliveries.FindAsync(id);

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

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/deliveries/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
                return NotFound();

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
