using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class TaxCalculationResultsRepository : ITaxCalculationResultsRepository
    {
        public Task AddTaxCalculationResultAsync(TaxCalculationResult taxCalculationResult)
        {
            throw new NotImplementedException();
        }
    }
}
