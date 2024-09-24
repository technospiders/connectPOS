using POS.Helper;
using System;

namespace POS.Data.Resources
{
    public class ConsigneeResource : ResourceParameters
    {
        public ConsigneeResource() : base("ConsigneeName")
        {

        }
        public Guid? Id { get; set; }
        public Guid CustomerId { get; set; }
        public string ConsigneeName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
    }
}
