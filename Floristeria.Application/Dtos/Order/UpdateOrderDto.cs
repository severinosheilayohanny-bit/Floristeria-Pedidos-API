using System.ComponentModel.DataAnnotations;

namespace Floristeria.Application.Dtos.Order
{
    public class UpdateOrderDto
    {
        [Required]
        [RegularExpression(
            "^(Pending|Confirmed|Cancelled|Completed)$",
            ErrorMessage = "El estado debe ser Pending, Confirmed, Cancelled o Completed.")]
        public string Status { get; set; } = string.Empty;
    }
}
