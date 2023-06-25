using AutoFixture;
using FluentAssertions;
using TaxCalculator.Core.Constants;
using TaxCalculator.WebApi.CustomValidationAttributes;

namespace TaxCalculator.WebApi.Tests.CustomValidationAttributes
{
    [TestFixture]
    public class AnnualIncomeRangeAttributeTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void IsValid_WithValidValue_ReturnsTrue()
        {
            // Arrange
            var attribute = new AnnualIncomeRangeAttribute();
            var validValue = _fixture.Create<decimal>();

            // Act
            var isValid = attribute.IsValid(validValue);

            // Assert
            isValid.Should().BeTrue();
        }

        [Test]
        public void IsValid_WithNullValue_ReturnsFalse()
        {
            // Arrange
            var attribute = new AnnualIncomeRangeAttribute();

            // Act
            var isValid = attribute.IsValid(null);

            // Assert
            isValid.Should().BeFalse();
        }

        [Test]
        public void IsValid_WithInvalidValue_ReturnsFalse()
        {
            // Arrange
            var attribute = new AnnualIncomeRangeAttribute();
            var invalidValue = NumericConstraints.MaxDecimalValue + 1m;

            // Act
            var isValid = attribute.IsValid(invalidValue);

            // Assert
            isValid.Should().BeFalse();
        }

        [Test]
        public void FormatErrorMessage_ReturnsCorrectErrorMessage()
        {
            // Arrange
            var attribute = new AnnualIncomeRangeAttribute();
            var propertyName = "AnnualIncome";

            // Act
            var errorMessage = attribute.FormatErrorMessage(propertyName);

            // Assert
            errorMessage.Should().Be($"{propertyName} must be between {0m} and {NumericConstraints.MaxDecimalValue}.");
        }
    }
}