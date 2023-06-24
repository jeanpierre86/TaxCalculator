using AutoMapper;
using TaxCalculator.Core.Constants;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.Entities.Enums;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Core.DTOs;
using TaxCalculator.Core.ServiceContracts;

namespace TaxCalculator.Core.Services
{
    public class GetIncomeTaxService : IGetIncomeTaxService
    {
        private readonly IMapper _mapper;
        private readonly IFlatRatesRepository _flatRatesRepository;
        private readonly IFlatValuesRepository _flatValuesRepository;
        private readonly IPostalCodeCalculationTypesRepository _postalCodeCalculationTypesRepository;
        private readonly IProgressiveRatesRepository _progressiveRatesRepository;

        public GetIncomeTaxService(
            IMapper mapper,
            IFlatRatesRepository flatRatesRepository,
            IFlatValuesRepository flatValuesRepository,
            IPostalCodeCalculationTypesRepository postalCodeCalculationTypesRepository,
            IProgressiveRatesRepository progressiveRatesRepository)
        {
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));

            _flatRatesRepository = flatRatesRepository ?? 
                throw new ArgumentNullException(nameof(flatRatesRepository));

            _flatValuesRepository = flatValuesRepository ?? 
                throw new ArgumentNullException(nameof(flatValuesRepository));

            _postalCodeCalculationTypesRepository = postalCodeCalculationTypesRepository ?? 
                throw new ArgumentNullException(nameof(postalCodeCalculationTypesRepository));

            _progressiveRatesRepository = progressiveRatesRepository ?? 
                throw new ArgumentNullException(nameof(progressiveRatesRepository));
        }

        public async Task<CalculateTaxResponseDTO> GetIncomeTaxAsync(
            CalculateTaxRequestDTO calculateTaxRequest)
        {
            if(calculateTaxRequest == null)
                throw new ArgumentNullException(nameof(calculateTaxRequest));

            if(string.IsNullOrWhiteSpace(calculateTaxRequest.PostalCode))
                throw new ArgumentException(nameof(calculateTaxRequest.PostalCode));

            var calculationType = await _postalCodeCalculationTypesRepository
                .GetPostalCodeCalculationTypeForCodeAsync(calculateTaxRequest.PostalCode);

            switch (calculationType.CalculationType)
            {
                case TaxCalculationType.Progressive:
                    return await GetProgressiveTaxAsync(calculateTaxRequest);

                case TaxCalculationType.FlatValue:
                    return await GetFlatValueTaxAsync(calculateTaxRequest);

                case TaxCalculationType.FlatRate:
                    return await GetFlatRateTaxAsync(calculateTaxRequest);

                default:
                    throw new InvalidOperationException(ErrorMessages.UnsupportedTaxCalculationType);
            }
        }

        private async Task<CalculateTaxResponseDTO> GetProgressiveTaxAsync(
            CalculateTaxRequestDTO calculateTaxRequest)
        {
            var progressiveRates = await _progressiveRatesRepository.GetProgressiveRatesAsync();

            var calculateTaxResult = _mapper.Map<CalculateTaxResponseDTO>(calculateTaxRequest);
            calculateTaxResult.IncomeTax = CalculateProgressiveTaxAmount(calculateTaxRequest.AnnualIncome, progressiveRates);

            return calculateTaxResult;
        }

        private decimal CalculateProgressiveTaxAmount(
            decimal annualIncome, 
            IEnumerable<ProgressiveRate> progressiveRates)
        {
            decimal taxAmount = 0m;
            decimal remainingIncome = annualIncome;

            foreach (var rate in progressiveRates)
            {
                decimal taxableIncome = Math.Min(remainingIncome, rate.AnnualIncomeTo - rate.AnnualIncomeFrom);
                decimal taxBracketAmount = rate.Rate * taxableIncome;
                taxAmount += taxBracketAmount;
                remainingIncome -= taxableIncome;

                if (remainingIncome <= 0m)
                    break;
            }

            return taxAmount;
        }

        private async Task<CalculateTaxResponseDTO> GetFlatValueTaxAsync(
            CalculateTaxRequestDTO calculateTaxRequest)
        {
            var flatValue = await _flatValuesRepository.GetFlatValueAsync();

            var incomeTax = calculateTaxRequest.AnnualIncome < flatValue.MaxAnnualIncome ?
                flatValue.RateForAnnualIncomeLessThanMax * calculateTaxRequest.AnnualIncome :
                flatValue.FlatAmountForAnnualIncomeExceedingMax;

            var calculateTaxResult = _mapper.Map<CalculateTaxResponseDTO>(calculateTaxRequest);
            calculateTaxResult.IncomeTax = incomeTax;

            return calculateTaxResult;
        }

        private async Task<CalculateTaxResponseDTO> GetFlatRateTaxAsync(
            CalculateTaxRequestDTO calculateTaxRequest)
        {
            var flatRate = await _flatRatesRepository.GetFlatRateAsync();

            var calculateTaxResult = _mapper.Map<CalculateTaxResponseDTO>(calculateTaxRequest);
            calculateTaxResult.IncomeTax = flatRate.Rate * calculateTaxRequest.AnnualIncome;

            return calculateTaxResult;
        }
    }
}
