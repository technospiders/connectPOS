using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompanyProfileTable_AddColInvoicePrintSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "InvoicePrintSize",
            table: "CompanyProfiles",
            type: "nvarchar(max)",
            nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "InvoicePrintSize",
            table: "CompanyProfiles");
        }
    }
}
