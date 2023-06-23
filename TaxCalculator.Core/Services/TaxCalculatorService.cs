using TaxCalculator.Core.DTOs;
using TaxCalculator.Core.ServiceContracts;

namespace TaxCalculator.Core.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        public Task<CalculateTaxResponseDTO> CalculateTaxAsync(CalculateTaxRequestDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
