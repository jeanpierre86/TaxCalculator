using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Infrastructure.DatabaseContext;
using TaxCalculator.Infrastructure.Repositories;

namespace TaxCalculator.Infrastructure.Tests.Repositories
{
    [TestFixture]
    public class TaxCalculationResultsRepositoryTests
    {
        private Fixture _fixture;
        private Mock<ApplicationDbContext> _dbContextMock;
        private ITaxCalculationResultsRepository _taxCalculationResultsRepository;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _taxCalculationResultsRepository = new TaxCalculationResultsRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task AddTaxCalculationResultAsync_WithValidResult_CallsDbContextMethods()
        {
            // Arrange
            var taxCalculationResult = _fixture.Create<TaxCalculationResult>();

            var taxCalculationResultsDbSetMock = new Mock<DbSet<TaxCalculationResult>>();
            _dbContextMock.Setup(c => c.TaxCalculationResults).Returns(taxCalculationResultsDbSetMock.Object);

            // Act
            await _taxCalculationResultsRepository.AddTaxCalculationResultAsync(taxCalculationResult);

            // Assert
            taxCalculationResultsDbSetMock.Verify(d => d.AddAsync(taxCalculationResult, default), Times.Once);
            _dbContextMock.Verify(d => d.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public void AddTaxCalculationResultAsync_WithNullResult_ThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Func<Task> action = async () => await _taxCalculationResultsRepository.AddTaxCalculationResultAsync(null);

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}