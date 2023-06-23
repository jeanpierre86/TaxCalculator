using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaxCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlatRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlatValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlatAmountForAnnualIncomeExceedingMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxAnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateForAnnualIncomeLessThanMax = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalCodeCalculationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodeCalculationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgressiveRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    AnnualIncomeFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnnualIncomeTo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressiveRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculationResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeTax = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculationResults", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FlatRates",
                columns: new[] { "Id", "Active", "DateCreated", "DateLastModified", "Rate" },
                values: new object[] { new Guid("435112d1-6a53-41e4-b001-5162e5b3fb3a"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.175m });

            migrationBuilder.InsertData(
                table: "FlatValues",
                columns: new[] { "Id", "Active", "DateCreated", "DateLastModified", "FlatAmountForAnnualIncomeExceedingMax", "MaxAnnualIncome", "RateForAnnualIncomeLessThanMax" },
                values: new object[] { new Guid("a806952a-ebf5-4b0e-ac54-e07b2a8c1411"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10000m, 200000m, 0.05m });

            migrationBuilder.InsertData(
                table: "PostalCodeCalculationTypes",
                columns: new[] { "Id", "Active", "CalculationType", "Code", "DateCreated", "DateLastModified" },
                values: new object[,]
                {
                    { new Guid("31afe152-c9ae-4238-971b-93b0fcd0dfd6"), true, 2, "7000", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6f95a24f-58c8-41a3-95ae-764b6e3b1bcb"), true, 0, "1000", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b11418b1-fa83-49c3-b508-d955306bc4dc"), true, 0, "7441", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bdf6175c-9076-457d-bc45-a75c82adcc03"), true, 1, "A100", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ProgressiveRates",
                columns: new[] { "Id", "Active", "AnnualIncomeFrom", "AnnualIncomeTo", "DateCreated", "DateLastModified", "Rate" },
                values: new object[,]
                {
                    { new Guid("0f7b7bed-b541-4e0d-8ffd-985f0af27fe2"), true, 372951m, 999999999999999.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.35m },
                    { new Guid("499e4639-08f3-40ae-a570-85e37c87dfbc"), true, 33951m, 82250m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.25m },
                    { new Guid("4dce90a9-2977-48b2-aae2-e6bab1a5182d"), true, 8351m, 33950m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.15m },
                    { new Guid("6d5849dd-58f3-4cd3-833e-9a6411ef917f"), true, 82251m, 171550m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.28m },
                    { new Guid("78ad321f-f881-42e7-8729-1cf415fe0127"), true, 0m, 8350m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.1m },
                    { new Guid("cba7eaf4-ce06-4e95-9d11-c869c745e932"), true, 171551m, 372950m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.33m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodeCalculationTypes_Code",
                table: "PostalCodeCalculationTypes",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatRates");

            migrationBuilder.DropTable(
                name: "FlatValues");

            migrationBuilder.DropTable(
                name: "PostalCodeCalculationTypes");

            migrationBuilder.DropTable(
                name: "ProgressiveRates");

            migrationBuilder.DropTable(
                name: "TaxCalculationResults");
        }
    }
}
