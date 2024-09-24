using System;

namespace POS.Data
{
    public class VehicleType : BaseEntity
    {
        public Guid Id { get; set; }
        public string VehicleTypeName { get; set; }
        public bool IsStatus { get; set; }
    }

}
