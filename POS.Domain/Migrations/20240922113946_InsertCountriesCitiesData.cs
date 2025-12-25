using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using System;

#nullable disable

namespace POS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InsertCountriesCitiesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //// Combine the current directory path and the SQL file path
            //var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations", "20240922113946_InsertCountriesCitiesData.sql");

            //// Read and execute the SQL file
            //if (File.Exists(sqlFile))
            //{
            //    var sql = File.ReadAllText(sqlFile);
            //    migrationBuilder.Sql(sql);
            //}
            //else
            //{
            //    throw new FileNotFoundException($"SQL file not found: {sqlFile}");
            //}
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
