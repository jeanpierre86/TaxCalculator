using TaxCalculator.Core.Domain.Entities;

namespace TaxCalculator.Core.Domain.RepositoryContracts
{
    public interface IFlatValuesRepository
    {
        Task<FlatValue> GetFlatValueAsync();
    }
}
