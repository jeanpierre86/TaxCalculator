using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class ProgressiveRatesRepository : IProgressiveRatesRepository
    {
        public Task<ProgressiveRate> GetProgressiveRateOnAnnualIncomeAsync(decimal annualIncome)
        {
            throw new NotImplementedException();
        }
    }
}
