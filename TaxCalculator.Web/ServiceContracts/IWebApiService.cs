using TaxCalculator.Web.ViewModels;

namespace TaxCalculator.Web.ServiceContracts
{
    public interface IWebApiService
    {
        Task<CalculateTaxResponse> GetTaxCalculationAsync(CalculateTaxRequest request);
    }
}
