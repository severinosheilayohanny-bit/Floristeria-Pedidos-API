using Floristeria.Application.Contract;
using Floristeria.Application.Dtos.Product;
using Floristeria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Floristeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(
            CreateProductDto dto)
        {
            var createdProduct = await _productService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetProduct),
                new { id = createdProduct.Id },
                createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(
            int id, UpdateProductDto dto)
        {
            var updated = await _productService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
