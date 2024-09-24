using System;

namespace POS.Data.Dto
{
    public class ReasonForExportDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsStatus { get; set; }
    }
}
