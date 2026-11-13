using System.ComponentModel.DataAnnotations;

namespace Floristeria.API.DTOs
{
    public class CreateOrderDetailDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
