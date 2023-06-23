namespace TaxCalculator.Core.Domain.Entities
{
    public class FlatValue : Entity
    {
        public decimal FlatAmountForAnnualIncomeExceedingMax { get; set; }
        public decimal MaxAnnualIncome { get; set; }
        public decimal RateForAnnualIncomeLessThanMax { get; set; }
    }
}
