using System;

namespace POS.Data.Dto
{
    public class WeightUnitDto
    {
        public Guid Id { get; set; }
        public string UnitName { get; set; }
        public bool IsStatus { get; set; }
    }
}
