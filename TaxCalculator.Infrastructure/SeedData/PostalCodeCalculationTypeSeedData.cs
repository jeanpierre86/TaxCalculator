using TaxCalculator.Core.Domain.Entities;

namespace TaxCalculator.Infrastructure.SeedData
{
    public static class FlatRateSeedData
    {
        public static IEnumerable<FlatRate> GetInitialData()
        {
            yield return new FlatRate
            {
                Id = Guid.Parse("435112D1-6A53-41E4-B001-5162E5B3FB3A"),
                Rate = 0.175m
            };
        }
    }
}
