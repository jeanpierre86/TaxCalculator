using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Web.ViewModels
{
    public class CalculateTaxRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string? PostalCode { get; set; }

        [Required]
        public decimal? AnnualIncome { get; set; }
    }
}
