using TaxCalculator.Core.DTOs;

namespace TaxCalculator.Core.ServiceContracts
{
    public interface IGetIncomeTaxService
    {
        Task<CalculateTaxResponseDTO> GetIncomeTaxAsync(CalculateTaxRequestDTO calculateTaxRequest);
    }
}
