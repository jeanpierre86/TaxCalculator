using TaxCalculator.Core.Domain.Entities;

namespace TaxCalculator.Core.Domain.RepositoryContracts
{
    public interface IFlatRatesRepository
    {
        Task<FlatRate> GetFlatRateAsync();
    }
}
