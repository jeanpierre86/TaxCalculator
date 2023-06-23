using TaxCalculator.Core.Constants;
using TaxCalculator.Core.Domain.Entities;

namespace TaxCalculator.Infrastructure.SeedData
{
    public static class ProgressiveRateSeedData
    {
        public static IEnumerable<ProgressiveRate> GetInitialData()
        {
            return new List<ProgressiveRate>()
            {
                new ProgressiveRate()
                {
                    Id = Guid.Parse("78AD321F-F881-42E7-8729-1CF415FE0127"),
                    AnnualIncomeFrom = 0m,
                    AnnualIncomeTo = 8350m,
                    Rate = 0.1m
                },
                new ProgressiveRate()
                {
                    Id = Guid.Parse("4DCE90A9-2977-48B2-AAE2-E6BAB1A5182D"),
                    AnnualIncomeFrom = 8351m,
                    AnnualIncomeTo = 33950m,
                    Rate = 0.15m
                },
                new ProgressiveRate()
                {
                    Id = Guid.Parse("499E4639-08F3-40AE-A570-85E37C87DFBC"),
                    AnnualIncomeFrom = 33951m,
                    AnnualIncomeTo = 82250m,
                    Rate = 0.25m
                },
                new ProgressiveRate()
                {
                    Id = Guid.Parse("6D5849DD-58F3-4CD3-833E-9A6411EF917F"),
                    AnnualIncomeFrom = 82251m,
                    AnnualIncomeTo = 171550m,
                    Rate = 0.28m
                },
                new ProgressiveRate()
                {
                    Id = Guid.Parse("CBA7EAF4-CE06-4E95-9D11-C869C745E932"),
                    AnnualIncomeFrom = 171551m,
                    AnnualIncomeTo = 372950m,
                    Rate = 0.33m
                },
                new ProgressiveRate()
                {
                    Id = Guid.Parse("0F7B7BED-B541-4E0D-8FFD-985F0AF27FE2"),
                    AnnualIncomeFrom = 372951m,
                    AnnualIncomeTo = NumericConstraints.MaxDecimalValue,
                    Rate = 0.35m
                }
            };
        }
    }
}
