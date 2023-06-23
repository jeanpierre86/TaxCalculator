namespace TaxCalculator.Core.DTOs
{
    public class CalculateTaxResponseDTO : ResponseBaseDTO
    {
        public string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal IncomeTax { get; set; }
    }
}
