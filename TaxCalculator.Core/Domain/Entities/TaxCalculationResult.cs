namespace TaxCalculator.Core.Domain.Entities
{
    public class TaxCalculationResult : Entity
    {
        public string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal IncomeTax { get; set; }
    }
}
