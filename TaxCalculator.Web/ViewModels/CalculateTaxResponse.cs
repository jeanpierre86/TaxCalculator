using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Web.ViewModels
{
    public class CalculateTaxResponse
    {
        public string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal IncomeTax { get; set; }
    }
}
