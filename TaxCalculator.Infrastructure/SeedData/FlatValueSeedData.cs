using TaxCalculator.Core.Domain.Entities;

namespace TaxCalculator.Infrastructure.SeedData
{
    public static class FlatValueSeedData
    {
        public static IEnumerable<FlatValue> GetInitialData()
        {
            yield return new FlatValue
            {
                Id = Guid.Parse("A806952A-EBF5-4B0E-AC54-E07B2A8C1411"),
                FlatAmountForAnnualIncomeExceedingMax = 10000m,
                MaxAnnualIncome = 200000m,
                RateForAnnualIncomeLessThanMax = 0.05m
            };
        }
    }
}
