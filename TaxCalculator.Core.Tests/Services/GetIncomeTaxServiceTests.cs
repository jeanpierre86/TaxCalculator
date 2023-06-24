using AutoMapper;
using FluentAssertions;
using Moq;
using TaxCalculator.Core.Domain.Entities.Enums;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Core.DTOs;
using TaxCalculator.Core.Services;
using AutoFixture;

namespace TaxCalculator.Core.Tests.Services
{
    public class GetIncomeTaxServiceTests
    {
        private IFixture _fixture;
        private Mock<IMapper> _mapperMock;
        private Mock<IFlatRatesRepository> _flatRatesRepositoryMock;
        private Mock<IFlatValuesRepository> _flatValuesRepositoryMock;
        private Mock<IPostalCodeCalculationTypesRepository> _postalCodeCalculationTypesRepositoryMock;
        private Mock<IProgressiveRatesRepository> _progressiveRatesRepositoryMock;
        private GetIncomeTaxService _getIncomeTaxService;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _flatRatesRepositoryMock = new Mock<IFlatRatesRepository>();
            _flatValuesRepositoryMock = new Mock<IFlatValuesRepository>();
            _postalCodeCalculationTypesRepositoryMock = new Mock<IPostalCodeCalculationTypesRepository>();
            _progressiveRatesRepositoryMock = new Mock<IProgressiveRatesRepository>();

            _getIncomeTaxService = new GetIncomeTaxService(
                _mapperMock.Object,
                _flatRatesRepositoryMock.Object,
                _flatValuesRepositoryMock.Object,
                _postalCodeCalculationTypesRepositoryMock.Object,
                _progressiveRatesRepositoryMock.Object);
        }

        [Test]
        public void Constructor_WithNullMapper_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new GetIncomeTaxService(
                null,
                _flatRatesRepositoryMock.Object,
                _flatValuesRepositoryMock.Object,
                _postalCodeCalculationTypesRepositoryMock.Object,
                _progressiveRatesRepositoryMock.Object);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*mapper*");
        }

        [Test]
        public void Constructor_WithNullFlatRatesRepository_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new GetIncomeTaxService(
                _mapperMock.Object,
                null,
                _flatValuesRepositoryMock.Object,
                _postalCodeCalculationTypesRepositoryMock.Object,
                _progressiveRatesRepositoryMock.Object);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*flatRatesRepository*");
        }

        [Test]
        public void Constructor_WithNullFlatValuesRepository_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new GetIncomeTaxService(
                _mapperMock.Object,
                _flatRatesRepositoryMock.Object,
                null,
                _postalCodeCalculationTypesRepositoryMock.Object,
                _progressiveRatesRepositoryMock.Object);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*flatValuesRepository*");
        }

        [Test]
        public void Constructor_WithNullPostalCodeCalculationTypesRepository_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new GetIncomeTaxService(
                _mapperMock.Object,
                _flatRatesRepositoryMock.Object,
                _flatValuesRepositoryMock.Object,
                null,
                _progressiveRatesRepositoryMock.Object);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*postalCodeCalculationTypesRepository*");
        }

        [Test]
        public void Constructor_WithNullProgressiveRatesRepository_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new GetIncomeTaxService(
                _mapperMock.Object,
                _flatRatesRepositoryMock.Object,
                _flatValuesRepositoryMock.Object,
                _postalCodeCalculationTypesRepositoryMock.Object,
                null);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*progressiveRatesRepository*");
        }

        [Test]
        public async Task GetIncomeTaxAsync_WhenCalculateTaxRequestIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            CalculateTaxRequestDTO calculateTaxRequest = null;

            // Act
            Func<Task> action = async () => await _getIncomeTaxService.GetIncomeTaxAsync(calculateTaxRequest);

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public async Task GetIncomeTaxAsync_WhenPostalCodeIsNullOrWhiteSpace_ShouldThrowArgumentException()
        {
            // Arrange
            var calculateTaxRequest = _fixture.Build<CalculateTaxRequestDTO>()
                .With(x => x.PostalCode, string.Empty)
                .Create();

            // Act
            Func<Task> action = async () => await _getIncomeTaxService.GetIncomeTaxAsync(calculateTaxRequest);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public async Task GetIncomeTaxAsync_WhenCalculationTypeIsProgressive_ShouldCallGetProgressiveTaxAsync()
        {
            // Arrange
            var calculateTaxRequest = _fixture.Create<CalculateTaxRequestDTO>();
            var postalCodeCalculationType = _fixture.Build<PostalCodeCalculationType>()
                .With(x => x.CalculationType, TaxCalculationType.Progressive)
                .Create();

            _postalCodeCalculationTypesRepositoryMock.Setup(x => x.GetPostalCodeCalculationTypeForCodeAsync(calculateTaxRequest.PostalCode))
                .ReturnsAsync(postalCodeCalculationType);

            _progressiveRatesRepositoryMock.Setup(x => x.GetProgressiveRatesAsync())
                .ReturnsAsync(new List<ProgressiveRate>());

            _mapperMock.Setup(x => x.Map<CalculateTaxResponseDTO>(It.IsAny<CalculateTaxRequestDTO>()))
                .Returns(new CalculateTaxResponseDTO());

            // Act
            await _getIncomeTaxService.GetIncomeTaxAsync(calculateTaxRequest);

            // Assert
            _postalCodeCalculationTypesRepositoryMock.Verify(x => x.GetPostalCodeCalculationTypeForCodeAsync(calculateTaxRequest.PostalCode), Times.Once);
            _progressiveRatesRepositoryMock.Verify(x => x.GetProgressiveRatesAsync(), Times.Once);

            _mapperMock.Verify(x => x.Map<CalculateTaxResponseDTO>(calculateTaxRequest), Times.Once);
            _mapperMock.VerifyAll();
        }

        [Test]
        public async Task GetIncomeTaxAsync_WhenCalculationTypeIsProgressive_ShouldCallCalculateCorrectProgressiveTaxAmount()
        {
            // Arrange
            var postalCodeCalculationType = _fixture.Build<PostalCodeCalculationType>()
                .With(x => x.CalculationType, TaxCalculationType.Progressive)
                .Create();

            var progressiveRates = new List<ProgressiveRate>
            {
                new ProgressiveRate { Rate = 0.18m, AnnualIncomeFrom = 0m, AnnualIncomeTo = 237100m },
                new ProgressiveRate { Rate = 0.26m, AnnualIncomeFrom = 237101m, AnnualIncomeTo = 370500m }
            };

            var calculateTaxRequest = _fixture.Build<CalculateTaxRequestDTO>()
                .With(x => x.AnnualIncome, 350000m)
                .Create();

            var firstBracket = progressiveRates.ElementAt(0);
            var secondBracket = progressiveRates.ElementAt(1);
            var firstBracketTax = firstBracket.Rate * firstBracket.AnnualIncomeTo;
            var expectedIncomeTax = firstBracketTax + (calculateTaxRequest.AnnualIncome - firstBracket.AnnualIncomeTo) * secondBracket.Rate;

            _postalCodeCalculationTypesRepositoryMock.Setup(x => x.GetPostalCodeCalculationTypeForCodeAsync(calculateTaxRequest.PostalCode))
                .ReturnsAsync(postalCodeCalculationType);

            _progressiveRatesRepositoryMock.Setup(x => x.GetProgressiveRatesAsync())
                .ReturnsAsync(progressiveRates);

            _mapperMock.Setup(x => x.Map<CalculateTaxResponseDTO>(It.IsAny<CalculateTaxRequestDTO>()))
                .Returns(new CalculateTaxResponseDTO());

            // Act
            var result = await _getIncomeTaxService.GetIncomeTaxAsync(calculateTaxRequest);

            // Assert
            result.IncomeTax.Should().Be(expectedIncomeTax);

            _mapperMock.Verify(x => x.Map<CalculateTaxResponseDTO>(calculateTaxRequest), Times.Once);
            _mapperMock.VerifyAll();
        }

        [Test]
        public async Task GetIncomeTaxAsync_WhenCalculationTypeIsFlatValue_ShouldCallGetFlatValueTaxAsync()
        {
            // Arrange
            var calculateTaxRequest = _fixture.Create<CalculateTaxRequestDTO>();
            var postalCodeCalculationType = _fixture.Build<PostalCodeCalculationType>()
                .With(x => x.CalculationType, TaxCalculationType.FlatValue)
                .Create();

            _postalCodeCalculationTypesRepositoryMock.Setup(x => x.GetPostalCodeCalculationTypeForCodeAsync(calculateTaxRequest.PostalCode))
                .ReturnsAsync(postalCodeCalculationType);

            _flatValuesRepositoryMock.Setup(x => x.GetFlatValueAsync())
                .ReturnsAsync(new FlatValue());

            _mapperMock.Setup(x => x.Map<CalculateTaxResponseDTO>(It.IsAny<CalculateTaxRequestDTO>()))
                .Returns(new CalculateTaxResponseDTO());

            // Act
            await _getIncomeTaxService.GetIncomeTaxAsync(calculateTaxRequest);

            // Assert
            _flatValuesRepositoryMock.Verify(x => x.GetFlatValueAsync(), Times.Once);

            _mapperMock.Verify(x => x.Map<CalculateTaxResponseDTO>(calculateTaxRequest), Times.Once);
            _mapperMock.VerifyAll();
        }

        [Test]
        public async Task GetIncomeTaxAsync_WhenCalculationTypeIsFlatRate_ShouldCallGetFlatRateTaxAsync()
        {
            // Arrange
            var calculateTaxRequest = _fixture.Create<CalculateTaxRequestDTO>();
            var postalCodeCalculationType = _fixture.Build<PostalCodeCalculationType>()
                .With(x => x.CalculationType, TaxCalculationType.FlatRate)
                .Create();

            _postalCodeCalculationTypesRepositoryMock.Setup(x => x.GetPostalCodeCalculationTypeForCodeAsync(calculateTaxRequest.PostalCode))
                .ReturnsAsync(postalCodeCalculationType);

            _flatRatesRepositoryMock.Setup(x => x.GetFlatRateAsync())
                .ReturnsAsync(new FlatRate());

            _mapperMock.Setup(x => x.Map<CalculateTaxResponseDTO>(It.IsAny<CalculateTaxRequestDTO>()))
                .Returns(new CalculateTaxResponseDTO());

            // Act
            await _getIncomeTaxService.GetIncomeTaxAsync(calculateTaxRequest);

            // Assert
            _flatRatesRepositoryMock.Verify(x => x.GetFlatRateAsync(), Times.Once);

            _mapperMock.Verify(x => x.Map<CalculateTaxResponseDTO>(calculateTaxRequest), Times.Once);
            _mapperMock.VerifyAll();
        }

        [Test]
        public async Task GetIncomeTaxAsync_WhenCalculationTypeIsInvalid_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var calculateTaxRequest = _fixture.Create<CalculateTaxRequestDTO>();
            var postalCodeCalculationType = _fixture.Build<PostalCodeCalculationType>()
                .With(x => x.CalculationType, (TaxCalculationType)(-1))
                .Create();

            _postalCodeCalculationTypesRepositoryMock.Setup(x => x.GetPostalCodeCalculationTypeForCodeAsync(calculateTaxRequest.PostalCode))
                .ReturnsAsync(postalCodeCalculationType);

            // Act
            Func<Task> action = async () => await _getIncomeTaxService.GetIncomeTaxAsync(calculateTaxRequest);

            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
