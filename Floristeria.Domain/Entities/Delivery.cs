using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Floristeria.Domain.Entities
{
    public class Delivery
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        public DateTime ScheduledDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending";

        public DateTime? DeliveredDate { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;
    }
}
