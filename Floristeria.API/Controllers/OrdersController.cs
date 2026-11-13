using Floristeria.API.Context;
using Floristeria.API.DTOs;
using Floristeria.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Floristeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: api/orders/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(
            CreateOrderDto dto)
        {
            var customerExists = await _context.Customers
                .AnyAsync(c => c.Id == dto.CustomerId);

            if (!customerExists)
                return BadRequest("El cliente no existe.");

            if (dto.OrderDetails == null ||
                dto.OrderDetails.Count == 0)
                return BadRequest("El pedido debe contener productos.");

            if (dto.OrderDetails
                .GroupBy(d => d.ProductId)
                .Any(g => g.Count() > 1))
                return BadRequest("No repitas productos en el pedido.");

            var productIds = dto.OrderDetails
                .Select(d => d.ProductId)
                .ToList();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending"
            };

            foreach (var item in dto.OrderDetails)
            {
                if (!products.TryGetValue(
                    item.ProductId, out var product))
                    return BadRequest(
                        $"El producto {item.ProductId} no existe.");

                if (!product.IsAvailable)
                    return BadRequest(
                        $"El producto {product.Name} no está disponible.");

                if (product.Stock < item.Quantity)
                    return BadRequest(
                        $"Stock insuficiente para {product.Name}.");

                var subtotal = product.Price * item.Quantity;

                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Subtotal = subtotal
                });

                order.Total += subtotal;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = order.Id },
                new
                {
                    order.Id,
                    order.CustomerId,
                    order.OrderDate,
                    order.Status,
                    order.Total
                });
        }

        // PUT: api/orders/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(
            int id, UpdateOrderDto dto)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            order.Status = dto.Status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/orders/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            _context.OrderDetails.RemoveRange(order.OrderDetails);
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
