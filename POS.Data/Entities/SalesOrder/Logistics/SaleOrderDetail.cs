
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data.Entities
{
    [Table("SaleOrderDetail", Schema = "logistics")]
    public class SaleOrderDetail
    {
        public Guid Id { get; set; }

        // FK to SalesOrder
        public Guid SaleOrderID { get; set; }
        [ForeignKey("SaleOrderID")]
        public SalesOrder SalesOrder { get; set; } // Navigation property

        // FK to Consignee
        public Guid ConsigneeId { get; set; }
        [ForeignKey("ConsigneeId")]
        public Consignee Consignee { get; set; } // Navigation property

        public int? NoOfBoxes { get; set; }

        // FK to PackingType
        public Guid? PackingTypeId { get; set; }
        [ForeignKey("PackingTypeId")]
        public PackingType PackingType { get; set; } // Navigation property

        public string Description { get; set; }

        // New Fields
        public decimal? WeightValue { get; set; }

        // FK to Currencies table
        public Guid? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public string CustomsValue { get; set; }
        public string InvoiceNo { get; set; }

        // FK to WeightUnit
        public Guid? WeightId { get; set; }
        [ForeignKey("WeightId")]
        public WeightUnit WeightUnit { get; set; } // Navigation property

        public decimal? Cubic { get; set; } // L * W * H

        // FK to ReasonForExport
        public Guid? ReasonForExportId { get; set; }
        [ForeignKey("ReasonForExportId")]
        public ReasonForExport ReasonForExport { get; set; } // Navigation property

        public string EForm { get; set; }
        public string DeliveryNotes { get; set; }
        public string ReceivedBy { get; set; }
        public string AirWayNumber { get; set; }
        public DateTime? CollectionDate { get; set; }
        public TimeSpan? CollectionReadyTime { get; set; }

        // FK to VehicleType
        public Guid? VehicleTypeId { get; set; }
        [ForeignKey("VehicleTypeId")]
        public VehicleType VehicleType { get; set; } // Navigation property

        public string SpecialInstructions { get; set; }

        // Collection of SaleOrderProductsItems related to this LogisticsSaleOrderDetail
        public ICollection<SaleOrderProductsItems> LogisticsSaleOrderProductsItems { get; set; }

    }

}
