using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class LogisticsRelatedDBChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "logistics");

            migrationBuilder.AddColumn<bool>(
                name: "IsLogisticsOrder",
                table: "SalesOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "InvoicePrintSize",
            //    table: "CompanyProfiles",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "ConsigneeAddresses",
                schema: "logistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsigneeAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsigneeAddresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConsigneeAddresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackingType",
                schema: "logistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackingType_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReasonForExport",
                schema: "logistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonForExport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReasonForExport_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTypes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeightUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightUnits_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consignee",
                schema: "logistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsigneeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVarified = table.Column<bool>(type: "bit", nullable: false),
                    IsUnsubscribe = table.Column<bool>(type: "bit", nullable: false),
                    ConsigneeProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsigneeAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consignee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consignee_ConsigneeAddresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalSchema: "logistics",
                        principalTable: "ConsigneeAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consignee_ConsigneeAddresses_ConsigneeAddressId",
                        column: x => x.ConsigneeAddressId,
                        principalSchema: "logistics",
                        principalTable: "ConsigneeAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consignee_ConsigneeAddresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalSchema: "logistics",
                        principalTable: "ConsigneeAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consignee_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consignee_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consignee_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consignee_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderDetail",
                schema: "logistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsigneeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoOfBoxes = table.Column<int>(type: "int", nullable: true),
                    PackingTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomsValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cubic = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReasonForExportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirWayNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CollectionReadyTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    VehicleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpecialInstructions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderDetail_Consignee_ConsigneeId",
                        column: x => x.ConsigneeId,
                        principalSchema: "logistics",
                        principalTable: "Consignee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrderDetail_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOrderDetail_PackingType_PackingTypeId",
                        column: x => x.PackingTypeId,
                        principalSchema: "logistics",
                        principalTable: "PackingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrderDetail_ReasonForExport_ReasonForExportId",
                        column: x => x.ReasonForExportId,
                        principalSchema: "logistics",
                        principalTable: "ReasonForExport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrderDetail_SalesOrders_SaleOrderID",
                        column: x => x.SaleOrderID,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrderDetail_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrderDetail_WeightUnits_WeightId",
                        column: x => x.WeightId,
                        principalTable: "WeightUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderProductsItems",
                schema: "logistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleOrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    COO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HSCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderProductsItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderProductsItems_SaleOrderDetail_SaleOrderDetailId",
                        column: x => x.SaleOrderDetailId,
                        principalSchema: "logistics",
                        principalTable: "SaleOrderDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrderProductsItems_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consignee_BillingAddressId",
                schema: "logistics",
                table: "Consignee",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Consignee_ConsigneeAddressId",
                schema: "logistics",
                table: "Consignee",
                column: "ConsigneeAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Consignee_CreatedBy",
                schema: "logistics",
                table: "Consignee",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Consignee_CustomerId",
                schema: "logistics",
                table: "Consignee",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Consignee_DeletedBy",
                schema: "logistics",
                table: "Consignee",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Consignee_ModifiedBy",
                schema: "logistics",
                table: "Consignee",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Consignee_ShippingAddressId",
                schema: "logistics",
                table: "Consignee",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsigneeAddresses_CityId",
                schema: "logistics",
                table: "ConsigneeAddresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsigneeAddresses_CountryId",
                schema: "logistics",
                table: "ConsigneeAddresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PackingType_CreatedBy",
                schema: "logistics",
                table: "PackingType",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonForExport_CreatedBy",
                schema: "logistics",
                table: "ReasonForExport",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetail_ConsigneeId",
                schema: "logistics",
                table: "SaleOrderDetail",
                column: "ConsigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetail_CurrencyId",
                schema: "logistics",
                table: "SaleOrderDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetail_PackingTypeId",
                schema: "logistics",
                table: "SaleOrderDetail",
                column: "PackingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetail_ReasonForExportId",
                schema: "logistics",
                table: "SaleOrderDetail",
                column: "ReasonForExportId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetail_SaleOrderID",
                schema: "logistics",
                table: "SaleOrderDetail",
                column: "SaleOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetail_VehicleTypeId",
                schema: "logistics",
                table: "SaleOrderDetail",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetail_WeightId",
                schema: "logistics",
                table: "SaleOrderDetail",
                column: "WeightId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderProductsItems_CreatedBy",
                schema: "logistics",
                table: "SaleOrderProductsItems",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderProductsItems_SaleOrderDetailId",
                schema: "logistics",
                table: "SaleOrderProductsItems",
                column: "SaleOrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypes_CreatedBy",
                table: "VehicleTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WeightUnits_CreatedBy",
                table: "WeightUnits",
                column: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleOrderProductsItems",
                schema: "logistics");

            migrationBuilder.DropTable(
                name: "SaleOrderDetail",
                schema: "logistics");

            migrationBuilder.DropTable(
                name: "Consignee",
                schema: "logistics");

            migrationBuilder.DropTable(
                name: "PackingType",
                schema: "logistics");

            migrationBuilder.DropTable(
                name: "ReasonForExport",
                schema: "logistics");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropTable(
                name: "WeightUnits");

            migrationBuilder.DropTable(
                name: "ConsigneeAddresses",
                schema: "logistics");

            migrationBuilder.DropColumn(
                name: "IsLogisticsOrder",
                table: "SalesOrders");

            //migrationBuilder.DropColumn(
            //    name: "InvoicePrintSize",
            //    table: "CompanyProfiles");
        }
    }
}
