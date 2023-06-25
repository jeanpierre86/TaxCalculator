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
    public class FlatRatesRepositoryTests
    {
        private Mock<ApplicationDbContext> _dbContextMock;
        private IFlatRatesRepository _flatRatesRepository;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _flatRatesRepository = new FlatRatesRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task GetFlatRateAsync_WithOneActiveFlatRate_ReturnsFlatRate()
        {
            // Arrange
            var flatRate = new FlatRate { Active = true };
            var flatRates = new List<FlatRate> { flatRate };
            var flatRatesDbSetMock = flatRates.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(c => c.FlatRates).Returns(flatRatesDbSetMock.Object);

            // Act
            var result = await _flatRatesRepository.GetFlatRateAsync();

            // Assert
            result.Should().Be(flatRate);
        }

        [Test]
        public void GetFlatRateAsync_WithNoActiveFlatRates_ThrowsInvalidOperationException()
        {
            // Arrange
            var flatRates = new List<FlatRate>();
            var flatRatesDbSetMock = flatRates.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(c => c.FlatRates).Returns(flatRatesDbSetMock.Object);

            // Act
            Func<Task> action = async () => await _flatRatesRepository.GetFlatRateAsync();

            // Assert
            action.Should().ThrowAsync<InvalidOperationException>();
        }

        [Test]
        public void GetFlatRateAsync_WithMultipleActiveFlatRates_ThrowsInvalidOperationException()
        {
            // Arrange
            var flatRates = new List<FlatRate>
            {
                new FlatRate { Active = true },
                new FlatRate { Active = true }
            };
            var flatRatesDbSetMock = flatRates.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(c => c.FlatRates).Returns(flatRatesDbSetMock.Object);

            // Act
            Func<Task> action = async () => await _flatRatesRepository.GetFlatRateAsync();

            // Assert
            action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}