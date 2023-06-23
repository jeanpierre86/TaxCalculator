using TaxCalculator.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Infrastructure.SeedData;
using TaxCalculator.Infrastructure.Constants;

namespace TaxCalculator.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<FlatRate> FlatRates { get; set; }
        public DbSet<FlatValue> FlatValues { get; set; }
        public DbSet<PostalCodeCalculationType> PostalCodeCalculationTypes { get; set; }
        public DbSet<ProgressiveRate> ProgressiveRates { get; set; }
        public DbSet<TaxCalculationResult> TaxCalculationResults { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigureEntities(builder);
            SeedInitialData(builder);
        }

        private static void ConfigureEntities(ModelBuilder builder)
        {
            builder.Entity<FlatRate>()
                .Property(x => x.Rate)
                .HasColumnType(SQLServerColumnTypes.Decimal184);

            builder.Entity<FlatValue>()
                .Property(x => x.FlatAmountForAnnualIncomeExceedingMax)
                .HasColumnType(SQLServerColumnTypes.Decimal182);

            builder.Entity<FlatValue>()
                .Property(x => x.MaxAnnualIncome)
                .HasColumnType(SQLServerColumnTypes.Decimal182);

            builder.Entity<FlatValue>()
                .Property(x => x.RateForAnnualIncomeLessThanMax)
                .HasColumnType(SQLServerColumnTypes.Decimal184);

            builder.Entity<ProgressiveRate>()
                .Property(x => x.Rate)
                .HasColumnType(SQLServerColumnTypes.Decimal184);

            builder.Entity<ProgressiveRate>()
                .Property(x => x.AnnualIncomeFrom)
                .HasColumnType(SQLServerColumnTypes.Decimal182);

            builder.Entity<ProgressiveRate>()
                .Property(x => x.AnnualIncomeTo)
                .HasColumnType(SQLServerColumnTypes.Decimal182);

            builder.Entity<TaxCalculationResult>()
                .Property(x => x.AnnualIncome)
                .HasColumnType(SQLServerColumnTypes.Decimal182);

            builder.Entity<TaxCalculationResult>()
                .Property(x => x.IncomeTax)
                .HasColumnType(SQLServerColumnTypes.Decimal188);

            builder.Entity<PostalCodeCalculationType>()
                .HasIndex(p => p.Code)
                .IsUnique();
        }

        private static void SeedInitialData(ModelBuilder builder)
        {
            builder.Entity<FlatRate>()
                .HasData(FlatRateSeedData.GetInitialData());

            builder.Entity<FlatValue>()
                .HasData(FlatValueSeedData.GetInitialData());

            builder.Entity<PostalCodeCalculationType>()
                .HasData(PostalCodeCalculationTypeSeedData.GetInitialData());

            builder.Entity<ProgressiveRate>()
                .HasData(ProgressiveRateSeedData.GetInitialData());
        }
    }
}
