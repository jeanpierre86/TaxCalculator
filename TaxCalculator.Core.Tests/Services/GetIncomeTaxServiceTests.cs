﻿using AutoMapper;
using FluentAssertions;
using Moq;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Core.DTOs;
using TaxCalculator.Core.Services;

namespace TaxCalculator.Core.Tests.Services
{
    public class GetIncomeTaxServiceTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IFlatRatesRepository> _flatRatesRepositoryMock;
        private Mock<IFlatValuesRepository> _flatValuesRepositoryMock;
        private Mock<IPostalCodeCalculationTypesRepository> _postalCodeCalculationTypesRepositoryMock;
        private Mock<IProgressiveRatesRepository> _progressiveRatesRepositoryMock;
        private GetIncomeTaxService _getIncomeTaxService;

        [SetUp]
        public void SetUp()
        {
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
    }
}