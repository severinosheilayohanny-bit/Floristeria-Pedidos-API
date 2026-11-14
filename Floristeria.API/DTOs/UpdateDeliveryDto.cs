using System.ComponentModel.DataAnnotations;

namespace Floristeria.API.DTOs
{
    public class UpdateDeliveryDto
    {
        [Required]
        [RegularExpression(
            "^(Pending|InTransit|Delivered|Cancelled)$",
            ErrorMessage = "El estado debe ser Pending, InTransit, Delivered o Cancelled.")]
        public string Status { get; set; } = string.Empty;

        public DateTime? DeliveredDate { get; set; }
    }
}
