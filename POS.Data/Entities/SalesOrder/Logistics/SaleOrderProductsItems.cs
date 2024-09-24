using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data.Entities
{
    [Table("SaleOrderProductsItems", Schema = "logistics")]
    public class SaleOrderProductsItems  : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid SaleOrderDetailId { get; set; } // FK to LogisticsSaleOrderDetail
        [ForeignKey("SaleOrderDetailId")]
        // Navigation properties
        public SaleOrderDetail SaleOrderDetail { get; set; } // FK navigation
        public string Description { get; set; } // Product description
        public int Qty { get; set; } // Quantity of items
        public decimal Price { get; set; } // Price of items
        public string COO { get; set; } // Country of Origin
        public string HSCode { get; set; } // HS Code (Harmonized System Code)
 
    }
}
