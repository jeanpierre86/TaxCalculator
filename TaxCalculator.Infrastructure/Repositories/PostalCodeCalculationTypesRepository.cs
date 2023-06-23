using Microsoft.EntityFrameworkCore;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Infrastructure.DatabaseContext;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class PostalCodeCalculationTypesRepository : IPostalCodeCalculationTypesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostalCodeCalculationTypesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<PostalCodeCalculationType> GetPostalCodeCalculationTypeForCodeAsync(string code)
        {
            var postalCodeCalculationTypes = await _dbContext.PostalCodeCalculationTypes
                .Where(x => x.Active && x.Code == code)
                .ToListAsync();

            if (postalCodeCalculationTypes.Count != 1)
                throw new InvalidOperationException($"Exactly one active postal code calculation type record for code: {code} should exist");

            return postalCodeCalculationTypes.First();
        }
    }
}
