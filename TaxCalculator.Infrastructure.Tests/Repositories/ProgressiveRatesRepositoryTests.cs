using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Infrastructure.DatabaseContext;
using TaxCalculator.Infrastructure.Repositories;

namespace TaxCalculator.Infrastructure.Tests.Repositories
{
    [TestFixture]
    public class ProgressiveRatesRepositoryTests
    {
        private Mock<ApplicationDbContext> _dbContextMock;
        private IProgressiveRatesRepository _progressiveRatesRepository;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _progressiveRatesRepository = new ProgressiveRatesRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task GetProgressiveRatesAsync_WithActiveRates_ReturnsOrderedRates()
        {
            // Arrange
            var progressiveRates = new List<ProgressiveRate>
            {
                new ProgressiveRate { Active = true, AnnualIncomeFrom = 10000 },
                new ProgressiveRate { Active = true, AnnualIncomeFrom = 5000 },
                new ProgressiveRate { Active = true, AnnualIncomeFrom = 15000 }
            };

            var progressiveRatesDbSetMock = progressiveRates.AsQueryable().BuildMockDbSet();
            _dbContextMock.Setup(c => c.ProgressiveRates).Returns(progressiveRatesDbSetMock.Object);

            // Act
            var result = await _progressiveRatesRepository.GetProgressiveRatesAsync();

            // Assert
            result.Should().BeEquivalentTo(progressiveRates.OrderBy(x => x.AnnualIncomeFrom));
        }
    }
}