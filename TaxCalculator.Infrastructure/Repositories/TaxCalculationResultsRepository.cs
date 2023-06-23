using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Infrastructure.DatabaseContext;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class TaxCalculationResultsRepository : ITaxCalculationResultsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TaxCalculationResultsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddTaxCalculationResultAsync(TaxCalculationResult taxCalculationResult)
        {
            await _dbContext.TaxCalculationResults.AddAsync(taxCalculationResult);
            await _dbContext.SaveChangesAsync();
        }
    }
}
