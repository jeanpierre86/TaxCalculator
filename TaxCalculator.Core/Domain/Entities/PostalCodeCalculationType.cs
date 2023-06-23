using TaxCalculator.Core.Domain.Entities.Enums;

namespace TaxCalculator.Core.Domain.Entities
{
    public class PostalCodeCalculationType : Entity
    {
        public string Code { get; set; }
        public TaxCalculationType CalculationType { get; set; }
    }
}
