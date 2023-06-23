using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class FlatValuesRepository : IFlatValuesRepository
    {
        public Task<FlatValue> GetFlatValueAsync()
        {
            throw new NotImplementedException();
        }
    }
}
