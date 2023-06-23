using AutoMapper;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Core.DTOs;
using TaxCalculator.Core.ServiceContracts;

namespace TaxCalculator.Core.Services
{
    public class SaveIncomeTaxService : ISaveIncomeTaxService
    {
        private readonly IMapper _mapper;
        private readonly ITaxCalculationResultsRepository _taxCalculationResultsRepository;

        public SaveIncomeTaxService(
            IMapper mapper,
            ITaxCalculationResultsRepository taxCalculationResultsRepository)
        {
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));

            _taxCalculationResultsRepository = taxCalculationResultsRepository ?? 
                throw new ArgumentNullException(nameof(taxCalculationResultsRepository));
        }

        public async Task SaveIncomeTaxAsync(CalculateTaxResponseDTO calculateTaxResponse)
        {
            await _taxCalculationResultsRepository.AddTaxCalculationResultAsync(
                _mapper.Map<TaxCalculationResult>(calculateTaxResponse));
        }
    }
}
