using Floristeria.Application.Contract;
using Floristeria.Application.Dtos.Customer;
using Floristeria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Floristeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerService.GetAllAsync();

            return Ok(customers);
        }

        // GET: api/customers/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(
            CreateCustomerDto dto)
        {
            var createdCustomer = await _customerService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetCustomer),
                new { id = createdCustomer.Id },
                createdCustomer);
        }

        // PUT: api/customers/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(
            int id, UpdateCustomerDto dto)
        {
            var updated = await _customerService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/customers/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
