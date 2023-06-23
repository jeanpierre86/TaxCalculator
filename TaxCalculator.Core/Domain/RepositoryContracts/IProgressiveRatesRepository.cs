using TaxCalculator.Core.Domain.Entities;

namespace TaxCalculator.Core.Domain.RepositoryContracts
{
    public interface IProgressiveRatesRepository
    {
        Task<IEnumerable<ProgressiveRate>> GetProgressiveRatesAsync();
    }
}
