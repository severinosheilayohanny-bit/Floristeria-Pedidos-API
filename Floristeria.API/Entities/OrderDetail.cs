using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Floristeria.API.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        // Relación con el pedido
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        // Relación con el producto
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}
