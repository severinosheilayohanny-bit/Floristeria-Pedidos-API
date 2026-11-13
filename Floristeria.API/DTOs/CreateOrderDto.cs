using System.ComponentModel.DataAnnotations;

namespace Floristeria.API.DTOs
{
    public class CreateOrderDto
    {
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }

        [Required]
        [MinLength(1)]
        public List<CreateOrderDetailDto> OrderDetails { get; set; }
            = new List<CreateOrderDetailDto>();
    }
}
