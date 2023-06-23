using TaxCalculator.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Infrastructure.SeedData;

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
            SeedInitialData(builder);
        }

        public static void SeedInitialData(ModelBuilder builder)
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
