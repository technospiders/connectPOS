using System;


namespace POS.Data.Dto
{
    public class SaleOrderProductsItemsDto
    {
        public Guid Id { get; set; }
        public Guid SaleOrderDetailId { get; set; }  // FK to SaleOrderDetail
        public string Description { get; set; }  // Product description
        public int Qty { get; set; }  // Quantity of items
        public decimal Price { get; set; }  // Price of items
        public string COO { get; set; }  // Country of Origin
        public string HSCode { get; set; }  // HS Code (Harmonized System Code)
    }

}
