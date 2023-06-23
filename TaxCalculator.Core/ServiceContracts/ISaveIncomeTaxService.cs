using TaxCalculator.Core.DTOs;

namespace TaxCalculator.Core.ServiceContracts
{
    public interface ISaveIncomeTaxService
    {
        Task SaveIncomeTaxAsync(CalculateTaxResponseDTO calculateTaxResponse);
    }
}
