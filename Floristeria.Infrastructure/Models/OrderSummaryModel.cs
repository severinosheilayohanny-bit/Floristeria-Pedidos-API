namespace Floristeria.Infrastructure.Models
{
    public class OrderSummaryModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal Total { get; set; }
    }
}
