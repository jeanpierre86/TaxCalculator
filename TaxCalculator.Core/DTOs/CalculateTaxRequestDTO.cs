namespace TaxCalculator.Core.DTOs
{
    public class CalculateTaxRequestDTO
    {
        public string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
    }
}
