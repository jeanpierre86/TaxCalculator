using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TaxCalculator.WebApi.ViewModels;
using TaxCalculator.Core.DTOs;
using TaxCalculator.Core.ServiceContracts;

namespace TaxCalculator.Web.Controllers
{
    /// <summary>
    /// Provides all the endpoints to do tax calculations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetIncomeTaxService _taxCalculatorService;
        private readonly ISaveIncomeTaxService _saveIncomeTaxService;

        public TaxCalculatorController(IMapper mapper, 
            IGetIncomeTaxService taxCalculatorService, 
            ISaveIncomeTaxService saveIncomeTaxService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _taxCalculatorService = taxCalculatorService ?? throw new ArgumentNullException(nameof(taxCalculatorService));
            _saveIncomeTaxService = saveIncomeTaxService ?? throw new ArgumentNullException(nameof(saveIncomeTaxService));
        }

        /// <summary>
        /// Returns the computed annual income tax based on the provide annual income
        /// </summary>
        /// <param name="calculateTaxRequest"></param>
        /// <returns></returns>
        [HttpPost("calculate")]
        public async Task<CalculateTaxResponseDTO> Calculate(CalculateTaxRequest calculateTaxRequest)
        {
            var calculateTaxResponseDTO = await _taxCalculatorService
                .GetIncomeTaxAsync(_mapper.Map<CalculateTaxRequestDTO>(calculateTaxRequest));

            await _saveIncomeTaxService.SaveIncomeTaxAsync(calculateTaxResponseDTO);

            return calculateTaxResponseDTO;
        }
    }
}
