using System;

namespace POS.Data.Dto
{
    public class ConsigneeAddressDto
    {
        public Guid? Id { get; set; }
        public Guid? ConsigneeId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string StateName { get; set; }
        public Guid? CityId { get; set; }
        public Guid? CountryId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}
