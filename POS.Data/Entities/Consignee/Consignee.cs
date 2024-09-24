using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data
{
    [Table("Consignee", Schema = "logistics")]
    public class Consignee : BaseEntity
    {
        public Guid Id { get; set; }
        public string ConsigneeName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsVarified { get; set; }
        public bool IsUnsubscribe { get; set; }
        public string ConsigneeProfile { get; set; }
        public string BusinessType { get; set; }
        public Guid ConsigneeAddressId { get; set; }
        [ForeignKey("ConsigneeAddressId")]
        public ConsigneeAddress ConsigneeAddress { get; set; }
        public Guid BillingAddressId { get; set; }
        [ForeignKey("BillingAddressId")]
        public ConsigneeAddress BillingAddress { get; set; }
        public Guid ShippingAddressId { get; set; }
        [ForeignKey("ShippingAddressId")]
        public ConsigneeAddress ShippingAddress { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }

        // Foreign Key to Customer
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } // Navigation property
    }
}
