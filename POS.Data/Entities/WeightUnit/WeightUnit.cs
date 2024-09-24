using System;

namespace POS.Data
{
    public class WeightUnit : BaseEntity
    {
        public Guid Id { get; set; }
        public string UnitName { get; set; }
        public bool IsStatus { get; set; }
    }

}
