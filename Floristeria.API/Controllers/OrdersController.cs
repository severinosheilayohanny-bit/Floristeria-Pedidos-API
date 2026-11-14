using Floristeria.Application.Contract;
using Floristeria.Application.Dtos.Order;
using Floristeria.Domain.Entities;
using Floristeria.Infrastructure.Interfaces;
using Floristeria.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Floristeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderSummaryRepository _orderSummaryRepository;

        public OrdersController(
            IOrderService orderService,
            IOrderSummaryRepository orderSummaryRepository)
        {
            _orderService = orderService;
            _orderSummaryRepository = orderSummaryRepository;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetAllAsync();

            return Ok(orders);
        }

        // GET: api/orders/summaries
        [HttpGet("summaries")]
        public async Task<ActionResult<IEnumerable<OrderSummaryModel>>> GetOrderSummaries()
        {
            var summaries =
                await _orderSummaryRepository.GetSummariesAsync();

            return Ok(summaries);
        }

        // GET: api/orders/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(
            CreateOrderDto dto)
        {
            var result = await _orderService.CreateAsync(dto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            if (result.Data is not Order createdOrder)
            {
                return StatusCode(
                    500,
                    "No se pudo recuperar el pedido creado.");
            }

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
            var updated = await _orderService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/orders/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
