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
    public class FlatValuesRepositoryTests
    {
        private Mock<ApplicationDbContext> _dbContextMock;
        private IFlatValuesRepository _flatValuesRepository;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _flatValuesRepository = new FlatValuesRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task GetFlatValueAsync_WithOneActiveFlatValue_ReturnsFlatValue()
        {
            // Arrange
            var FlatValue = new FlatValue { Active = true };
            var FlatValues = new List<FlatValue> { FlatValue };
            var FlatValuesDbSetMock = FlatValues.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(c => c.FlatValues).Returns(FlatValuesDbSetMock.Object);

            // Act
            var result = await _flatValuesRepository.GetFlatValueAsync();

            // Assert
            result.Should().Be(FlatValue);
        }

        [Test]
        public void GetFlatValueAsync_WithNoActiveFlatValues_ThrowsInvalidOperationException()
        {
            // Arrange
            var FlatValues = new List<FlatValue>();
            var FlatValuesDbSetMock = FlatValues.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(c => c.FlatValues).Returns(FlatValuesDbSetMock.Object);

            // Act
            Func<Task> action = async () => await _flatValuesRepository.GetFlatValueAsync();

            // Assert
            action.Should().ThrowAsync<InvalidOperationException>();
        }

        [Test]
        public void GetFlatValueAsync_WithMultipleActiveFlatValues_ThrowsInvalidOperationException()
        {
            // Arrange
            var FlatValues = new List<FlatValue>
            {
                new FlatValue { Active = true },
                new FlatValue { Active = true }
            };
            var FlatValuesDbSetMock = FlatValues.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(c => c.FlatValues).Returns(FlatValuesDbSetMock.Object);

            // Act
            Func<Task> action = async () => await _flatValuesRepository.GetFlatValueAsync();

            // Assert
            action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
