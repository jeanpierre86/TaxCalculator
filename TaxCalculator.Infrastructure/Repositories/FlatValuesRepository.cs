using Microsoft.EntityFrameworkCore;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Infrastructure.DatabaseContext;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class FlatValuesRepository : IFlatValuesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FlatValuesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<FlatValue> GetFlatValueAsync()
        {
            var flatValues = await _dbContext.FlatValues
                .Where(x => x.Active)
                .ToListAsync();

            if (flatValues.Count != 1)
                throw new InvalidOperationException("Exactly one active flat value record should exist");

            return flatValues.First();
        }
    }
}
