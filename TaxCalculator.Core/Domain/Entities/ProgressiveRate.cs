namespace TaxCalculator.Core.Domain.Entities
{
    public class ProgressiveRate : Entity
    {
        public decimal Rate { get; set; }
        public decimal AnnualIncomeFrom { get; set; }
        public decimal AnnualIncomeTo { get; set; }
    }
}
