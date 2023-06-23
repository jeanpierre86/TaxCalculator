using System.ComponentModel.DataAnnotations;
using TaxCalculator.WebApi.CustomValidationAttributes;

namespace TaxCalculator.WebApi.ViewModels
{
    public class CalculateTaxRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string PostalCode { get; set; }

        [Required]
        [AnnualIncomeRange]
        public decimal AnnualIncome { get; set; }
    }
}
