using TaxCalculator.Core.DTOs;

namespace TaxCalculator.Core.ServiceContracts
{
    public interface ITaxCalculatorService
    {
        Task<CalculateTaxResponseDTO> CalculateTaxAsync(CalculateTaxRequestDTO request);
    }
}
