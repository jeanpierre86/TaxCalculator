using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.Entities.Enums;

namespace TaxCalculator.Infrastructure.SeedData
{
    public static class PostalCodeCalculationTypeSeedData
    {
        public static IEnumerable<PostalCodeCalculationType> GetInitialData()
        {
            return new List<PostalCodeCalculationType>()
            {
                new PostalCodeCalculationType()
                {
                    Id = Guid.Parse("B11418B1-FA83-49C3-B508-D955306BC4DC"),
                    Code = "7441",
                    CalculationType = TaxCalculationType.Progressive
                },
                new PostalCodeCalculationType()
                {
                    Id = Guid.Parse("BDF6175C-9076-457D-BC45-A75C82ADCC03"),
                    Code = "A100",
                    CalculationType = TaxCalculationType.FlatValue
                },
                new PostalCodeCalculationType()
                {
                    Id = Guid.Parse("31AFE152-C9AE-4238-971B-93B0FCD0DFD6"),
                    Code = "7000",
                    CalculationType = TaxCalculationType.FlatRate
                },
                new PostalCodeCalculationType()
                {
                    Id = Guid.Parse("6F95A24F-58C8-41A3-95AE-764B6E3B1BCB"),
                    Code = "1000",
                    CalculationType = TaxCalculationType.Progressive
                }
            };
        }
    }
}
