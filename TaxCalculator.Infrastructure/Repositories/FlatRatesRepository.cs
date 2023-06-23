using Microsoft.EntityFrameworkCore;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Infrastructure.DatabaseContext;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class FlatRatesRepository : IFlatRatesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FlatRatesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<FlatRate> GetFlatRateAsync()
        {
            var flatRates = await _dbContext.FlatRates
                .Where(x => x.Active)
                .ToListAsync();

            if (flatRates.Count != 1)
                throw new InvalidOperationException("Exactly one active flat rate record should exist");

            return flatRates.First();
        }
    }
}
