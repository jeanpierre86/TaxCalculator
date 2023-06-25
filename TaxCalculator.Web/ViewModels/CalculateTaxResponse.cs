namespace TaxCalculator.Web.ViewModels
{
    public class CalculateTaxResponse
    {
        public IEnumerable<string>? Errors { get; set; }
        public string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal IncomeTax { get; set; }
    }
}
