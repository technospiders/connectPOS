using System;
using System.Collections.Generic;

namespace POS.Data.Dto
{
    public class ConsigneeDto
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
        public string ImageUrl { get; set; }
        public string Country { get; set; }
        public Guid ConsigneeAddressId { get; set; }
        public ConsigneeAddressDto ConsigneeAddress { get; set; }
        public Guid? BillingAddressId { get; set; }
        public ConsigneeAddressDto BillingAddress { get; set; }
        public Guid? ShippingAddressId { get; set; }
        public ConsigneeAddressDto ShippingAddress { get; set; }

        // Add Customer Information
        public Guid CustomerId { get; set; }
        public CustomerDto Customer { get; set; }  // Navigation property
    }
}
