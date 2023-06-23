using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class PostalCodeCalculationTypesRepository : IPostalCodeCalculationTypesRepository
    {
        public Task<PostalCodeCalculationType> GetPostalCodeCalculationTypeForCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
