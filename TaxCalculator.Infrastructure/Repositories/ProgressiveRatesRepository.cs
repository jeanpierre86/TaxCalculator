using Microsoft.EntityFrameworkCore;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Infrastructure.DatabaseContext;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class ProgressiveRatesRepository : IProgressiveRatesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProgressiveRatesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<ProgressiveRate>> GetProgressiveRatesAsync()
        {
            return await _dbContext.ProgressiveRates
                .Where(x => x.Active)
                .OrderBy(x => x.AnnualIncomeFrom)
                .ToListAsync();
        }
    }
}
