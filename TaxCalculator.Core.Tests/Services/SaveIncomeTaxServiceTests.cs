using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Core.DTOs;
using TaxCalculator.Core.Services;

namespace TaxCalculator.Core.Tests.Services
{
    public class SaveIncomeTaxServiceTests
    {
        private IFixture _fixture;
        private Mock<IMapper> _mapperMock;
        private Mock<ITaxCalculationResultsRepository> _taxCalculationResultsRepositoryMock;
        private SaveIncomeTaxService _saveIncomeTaxService;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _taxCalculationResultsRepositoryMock = new Mock<ITaxCalculationResultsRepository>();

            _saveIncomeTaxService = new SaveIncomeTaxService(
                _mapperMock.Object,
                _taxCalculationResultsRepositoryMock.Object);
        }

        [Test]
        public void Constructor_WithNullMapper_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new SaveIncomeTaxService(
                null,
                _taxCalculationResultsRepositoryMock.Object);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*mapper*");
        }

        [Test]
        public void Constructor_WithNullTaxCalculationResultsRepository_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new SaveIncomeTaxService(
                _mapperMock.Object,
                null);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*taxCalculationResultsRepository*");
        }

        [Test]
        public async Task SaveIncomeTaxAsync_WhenCalculateTaxResponseProvided_ShouldCallRepositoryToSave()
        {
            //Arrange
            var calculateTaxResponse = _fixture.Create<CalculateTaxResponseDTO>();

            _mapperMock.Setup(x => x.Map<TaxCalculationResult>(It.IsAny<CalculateTaxResponseDTO>()))
                .Returns(new TaxCalculationResult());

            //Act
            await _saveIncomeTaxService.SaveIncomeTaxAsync(calculateTaxResponse);

            //Assert
            _mapperMock.Verify(x => x.Map<TaxCalculationResult>(calculateTaxResponse), Times.Once);
            _mapperMock.VerifyAll();
        }
    }
}
