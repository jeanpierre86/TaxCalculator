using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaxCalculator.Web.Controllers;
using TaxCalculator.Web.ServiceContracts;
using TaxCalculator.Web.ViewModels;

namespace TaxCalculator.Web.Tests.Controllers
{
    [TestFixture]
    public class TaxCalculatorControllerTests
    {
        private Mock<IWebApiService> _webApiServiceMock;
        private TaxCalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _webApiServiceMock = new Mock<IWebApiService>();
            _controller = new TaxCalculatorController(_webApiServiceMock.Object);
        }

        [Test]
        public void Index_ReturnsViewWithCalculateTaxRequest()
        {
            // Act
            var result = _controller.Index();

            // Assert
            result.Should().BeAssignableTo<ViewResult>();
            result.As<ViewResult>().Model.Should().BeOfType<CalculateTaxRequest>();
        }

        [Test]
        public async Task GetIncomeTax_WithValidModel_ReturnsJsonResponse()
        {
            // Arrange
            var fixture = new Fixture();
            var calculateTaxRequest = fixture.Create<CalculateTaxRequest>();
            var incomeTax = fixture.Create<CalculateTaxResponse>();
            _webApiServiceMock.Setup(x => x.GetTaxCalculationAsync(calculateTaxRequest))
                .ReturnsAsync(incomeTax);

            // Act
            var result = await _controller.GetIncomeTax(calculateTaxRequest) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(incomeTax);
        }

        [Test]
        public async Task GetIncomeTax_WithInvalidModel_ReturnsJsonErrorResponse()
        {
            // Arrange
            var fixture = new Fixture();
            var calculateTaxRequest = fixture.Create<CalculateTaxRequest>();
            _controller.ModelState.AddModelError("AnnualIncome", "Annual income is required.");

            // Act
            var result = await _controller.GetIncomeTax(calculateTaxRequest) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeAssignableTo<CalculateTaxResponse>();
            var response = result.Value as CalculateTaxResponse;
            response.Errors.Should().NotBeNull();
            response.Errors.Should().HaveCount(1);
            response.Errors.First().Should().Be("Annual income is required.");
        }
    }
}