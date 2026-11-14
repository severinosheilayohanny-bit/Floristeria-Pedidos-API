using Floristeria.API.DTOs;
using Floristeria.Domain.Entities;
using Floristeria.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Floristeria.Infrastructure.Interfaces;
using Floristeria.Infrastructure.Models;

namespace Floristeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderSummaryRepository _orderSummaryRepository;

        public OrdersController(
            IOrderRepository orderRepository,
            IOrderSummaryRepository orderSummaryRepository)
        {
            _orderRepository = orderRepository;
            _orderSummaryRepository = orderSummaryRepository;
        }


        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderRepository.GetAllAsync();

            return Ok(orders);
        }

        // GET: api/orders/summaries
        [HttpGet("summaries")]
        public async Task<ActionResult<IEnumerable<OrderSummaryModel>>> GetOrderSummaries()
        {
            var summaries = await _orderSummaryRepository.GetSummariesAsync();

            return Ok(summaries);
        }

        // GET: api/orders/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(
            CreateOrderDto dto)
        {
            var customerExists =
                await _orderRepository.CustomerExistsAsync(dto.CustomerId);

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

            var products =
                await _orderRepository.GetProductsByIdsAsync(productIds);

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

            var createdOrder = await _orderRepository.AddAsync(order);

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = createdOrder.Id },
                new
                {
                    createdOrder.Id,
                    createdOrder.CustomerId,
                    createdOrder.OrderDate,
                    createdOrder.Status,
                    createdOrder.Total
                });
        }

        // PUT: api/orders/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(
            int id, UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            order.Status = dto.Status;

            var updated = await _orderRepository.UpdateAsync(order);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/orders/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderRepository.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
