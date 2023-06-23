using TaxCalculator.Core.Domain.Entities;

namespace TaxCalculator.Core.Domain.RepositoryContracts
{
    public interface IPostalCodeCalculationTypesRepository
    {
        Task<PostalCodeCalculationType> GetPostalCodeCalculationTypeForCodeAsync(string code);
    }
}
