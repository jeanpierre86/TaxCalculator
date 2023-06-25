using AutoFixture;
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
    public class PostalCodeCalculationTypesRepositoryTests
    {
        private Fixture _fixture;
        private Mock<ApplicationDbContext> _dbContextMock;
        private IPostalCodeCalculationTypesRepository _postalCodeCalculationTypesRepository;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _postalCodeCalculationTypesRepository = new PostalCodeCalculationTypesRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task GetPostalCodeCalculationTypeAsync_WithOneActivePostalCodeCalculationType_ReturnsPostalCodeCalculationType()
        {
            // Arrange
            var postalCode = _fixture.Create<string>();
            var PostalCodeCalculationType = new PostalCodeCalculationType { Active = true, Code = postalCode };
            var PostalCodeCalculationTypes = new List<PostalCodeCalculationType> { PostalCodeCalculationType };
            var PostalCodeCalculationTypesDbSetMock = PostalCodeCalculationTypes.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(c => c.PostalCodeCalculationTypes).Returns(PostalCodeCalculationTypesDbSetMock.Object);

            // Act
            var result = await _postalCodeCalculationTypesRepository.GetPostalCodeCalculationTypeForCodeAsync(postalCode);

            // Assert
            result.Should().Be(PostalCodeCalculationType);
        }

        [Test]
        public void GetPostalCodeCalculationTypeAsync_WithNoActivePostalCodeCalculationTypes_ThrowsInvalidOperationException()
        {
            // Arrange
            var PostalCodeCalculationTypes = new List<PostalCodeCalculationType>();
            var PostalCodeCalculationTypesDbSetMock = PostalCodeCalculationTypes.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(c => c.PostalCodeCalculationTypes).Returns(PostalCodeCalculationTypesDbSetMock.Object);

            // Act
            Func<Task> action = async () => 
            await _postalCodeCalculationTypesRepository.GetPostalCodeCalculationTypeForCodeAsync(_fixture.Create<string>());

            // Assert
            action.Should().ThrowAsync<InvalidOperationException>();
        }

        [Test]
        public void GetPostalCodeCalculationTypeAsync_WithMultipleActivePostalCodeCalculationTypes_ThrowsInvalidOperationException()
        {
            // Arrange
            var PostalCodeCalculationTypes = new List<PostalCodeCalculationType>
            {
                new PostalCodeCalculationType { Active = true },
                new PostalCodeCalculationType { Active = true }
            };
            var PostalCodeCalculationTypesDbSetMock = PostalCodeCalculationTypes.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(c => c.PostalCodeCalculationTypes).Returns(PostalCodeCalculationTypesDbSetMock.Object);

            // Act
            Func<Task> action = async () =>
            await _postalCodeCalculationTypesRepository.GetPostalCodeCalculationTypeForCodeAsync(_fixture.Create<string>());

            // Assert
            action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
