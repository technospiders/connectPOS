
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using POS.Data.Entities;

namespace POS.Data.Dto
{
    public class SaleOrderDetailDto
    {
        public Guid Id { get; set; }
        public Guid SaleOrderID { get; set; }  // FK to SalesOrder
        public Guid ConsigneeId { get; set; }  // FK to Consignee
        public ConsigneeDto Consignee { get; set; }
        public int? NoOfBoxes { get; set; }
        public Guid? PackingTypeId { get; set; }  // FK to PackingType
        public PackingTypeDto PackingType { get; set; }  // FK to PackingType
        public string Description { get; set; }
        // New Fields
        public decimal? WeightValue { get; set; }
        public Guid? CurrencyId { get; set; }
        public CurrencyDto Currency { get; set; }
        public string CustomsValue { get; set; }
        public string InvoiceNo { get; set; }
        public Guid? WeightId { get; set; }  // FK to WeightUnit
        public decimal? Cubic { get; set; }  // L * W * H
        public Guid? ReasonForExportId { get; set; }  // FK to ReasonForExport
        public ReasonForExportDto ReasonForExport { get; set; }  // FK to ReasonForExport
        public string EForm { get; set; }
        public string DeliveryNotes { get; set; }
        public string ReceivedBy { get; set; }
        public string AirWayNumber { get; set; }
        public DateTime? CollectionDate { get; set; }
        public TimeSpan? CollectionReadyTime { get; set; }
        public Guid? VehicleTypeId { get; set; }  // FK to VehicleType
        public string SpecialInstructions { get; set; }

        public ICollection<SaleOrderProductsItems> LogisticsSaleOrderProductsItems { get; set; }

    }
}
