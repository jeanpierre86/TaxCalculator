using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class FlatRatesRepository : IFlatRatesRepository
    {
        public Task<FlatRate> GetFlatRateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
