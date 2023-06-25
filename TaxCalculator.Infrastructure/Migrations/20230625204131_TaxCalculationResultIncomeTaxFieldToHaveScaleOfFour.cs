using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TaxCalculationResultIncomeTaxFieldToHaveScaleOfFour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "IncomeTax",
                table: "TaxCalculationResults",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "IncomeTax",
                table: "TaxCalculationResults",
                type: "decimal(18,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");
        }
    }
}
