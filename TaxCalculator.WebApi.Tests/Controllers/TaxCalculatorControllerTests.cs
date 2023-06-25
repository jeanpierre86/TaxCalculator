using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using TaxCalculator.Core.DTOs;
using TaxCalculator.Core.ServiceContracts;
using TaxCalculator.Web.Controllers;
using TaxCalculator.WebApi.ViewModels;

namespace TaxCalculator.Web.Tests.Controllers
{
    [TestFixture]
    public class TaxCalculatorControllerTests
    {
        private Fixture _fixture;
        private Mock<IMapper> _mapperMock;
        private Mock<IGetIncomeTaxService> _getIncomeTaxServiceMock;
        private Mock<ISaveIncomeTaxService> _saveIncomeTaxServiceMock;
        private TaxCalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _getIncomeTaxServiceMock = new Mock<IGetIncomeTaxService>();
            _saveIncomeTaxServiceMock = new Mock<ISaveIncomeTaxService>();

            _controller = new TaxCalculatorController(
                _mapperMock.Object,
                _getIncomeTaxServiceMock.Object,
                _saveIncomeTaxServiceMock.Object
            );
        }

        [Test]
        public async Task Calculate_WithValidRequest_ReturnsCalculateTaxResponseDTO()
        {
            // Arrange
            var calculateTaxRequest = _fixture.Create<CalculateTaxRequest>();
            var calculateTaxRequestDto = _fixture.Create<CalculateTaxRequestDTO>();
            var calculateTaxResponseDto = _fixture.Create<CalculateTaxResponseDTO>();

            _mapperMock.Setup(m => m.Map<CalculateTaxRequestDTO>(calculateTaxRequest))
                .Returns(calculateTaxRequestDto);

            _getIncomeTaxServiceMock.Setup(s => s.GetIncomeTaxAsync(calculateTaxRequestDto))
                .ReturnsAsync(calculateTaxResponseDto);

            // Act
            var result = await _controller.Calculate(calculateTaxRequest);

            // Assert
            result.Should().BeEquivalentTo(calculateTaxResponseDto);
        }

        [Test]
        public void Calculate_WithNullRequest_ThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Func<Task> action = async () => await _controller.Calculate(null);

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public async Task Calculate_WithValidRequest_CallsSaveIncomeTaxService()
        {
            // Arrange
            var calculateTaxRequest = _fixture.Create<CalculateTaxRequest>();
            var calculateTaxRequestDto = _fixture.Create<CalculateTaxRequestDTO>();
            var calculateTaxResponseDto = _fixture.Create<CalculateTaxResponseDTO>();

            _mapperMock.Setup(m => m.Map<CalculateTaxRequestDTO>(calculateTaxRequest))
                .Returns(calculateTaxRequestDto);

            _getIncomeTaxServiceMock.Setup(s => s.GetIncomeTaxAsync(calculateTaxRequestDto))
                .ReturnsAsync(calculateTaxResponseDto);

            // Act
            await _controller.Calculate(calculateTaxRequest);

            // Assert
            _saveIncomeTaxServiceMock.Verify(s => s.SaveIncomeTaxAsync(calculateTaxResponseDto), Times.Once);
        }
    }
}