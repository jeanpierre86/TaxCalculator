using AutoMapper;
using FluentAssertions;
using Moq;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Core.Services;

namespace TaxCalculator.Core.Tests.Services
{
    public class SaveIncomeTaxServiceTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ITaxCalculationResultsRepository> _taxCalculationResultsRepositoryMock;
        private SaveIncomeTaxService _saveIncomeTaxService;

        [SetUp]
        public void SetUp()
        {
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
    }
}
