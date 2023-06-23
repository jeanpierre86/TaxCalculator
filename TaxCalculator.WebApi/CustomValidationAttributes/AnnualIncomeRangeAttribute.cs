using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.WebApi.CustomValidationAttributes
{
    public class AnnualIncomeRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if (!decimal.TryParse(value.ToString(), out decimal decimalValue))
                return false;

            return decimalValue >= 0m && decimalValue <= decimal.MaxValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be between {0m} and {decimal.MaxValue}.";
        }
    }
}
