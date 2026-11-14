using System.ComponentModel.DataAnnotations;

namespace Floristeria.Application.Dtos.Delivery
{
    public class CreateDeliveryDto
    {
        [Range(1, int.MaxValue)]
        public int OrderId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime ScheduledDate { get; set; }
    }
}
