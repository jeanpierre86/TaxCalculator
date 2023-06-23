using AutoMapper;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Core.DTOs;
using TaxCalculator.Core.ServiceContracts;

namespace TaxCalculator.Core.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        private readonly IMapper _mapper;
        private readonly IFlatRatesRepository _flatRatesRepository;
        private readonly IFlatValuesRepository _flatValuesRepository;
        private readonly IPostalCodeCalculationTypesRepository _postalCodeCalculationTypesRepository;
        private readonly IProgressiveRatesRepository _progressiveRatesRepository;
        private readonly ITaxCalculationResultsRepository _taxCalculationResultsRepository;

        public TaxCalculatorService(
            IMapper mapper,
            IFlatRatesRepository flatRatesRepository,
            IFlatValuesRepository flatValuesRepository,
            IPostalCodeCalculationTypesRepository postalCodeCalculationTypesRepository,
            IProgressiveRatesRepository progressiveRatesRepository,
            ITaxCalculationResultsRepository taxCalculationResultsRepository)
        {
            _mapper = mapper;
            _flatRatesRepository = flatRatesRepository;
            _flatValuesRepository = flatValuesRepository;
            _postalCodeCalculationTypesRepository = postalCodeCalculationTypesRepository;
            _progressiveRatesRepository = progressiveRatesRepository;
            _taxCalculationResultsRepository = taxCalculationResultsRepository;
        }

        public async Task<CalculateTaxResponseDTO> CalculateTaxAsync(CalculateTaxRequestDTO request)
        {
            throw new NotImplementedException();
            //var calculationType = await _postalCodeCalculationTypesRepository
            //    .GetPostalCodeCalculationTypeForCodeAsync(request.PostalCode);
        }
    }
}
