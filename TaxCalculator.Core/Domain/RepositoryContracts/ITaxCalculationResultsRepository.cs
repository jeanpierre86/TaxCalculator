using TaxCalculator.Core.Domain.Entities;

namespace TaxCalculator.Core.Domain.RepositoryContracts
{
    public interface ITaxCalculationResultsRepository
    {
        Task AddTaxCalculationResultAsync(TaxCalculationResult taxCalculationResult);
    }
}
