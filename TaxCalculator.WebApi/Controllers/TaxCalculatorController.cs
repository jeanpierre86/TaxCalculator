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
        private readonly ITaxCalculatorService _taxCalculatorService;

        public TaxCalculatorController(IMapper mapper, ITaxCalculatorService taxCalculatorService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _taxCalculatorService = taxCalculatorService ?? throw new ArgumentNullException(nameof(taxCalculatorService));
        }

        /// <summary>
        /// Returns the computed annual income tax based on the provide annual income
        /// </summary>
        /// <param name="calculateTaxRequest"></param>
        /// <returns></returns>
        [HttpPost("calculate")]
        public async Task<CalculateTaxResponseDTO> Calculate(CalculateTaxRequest calculateTaxRequest)
        {
            return await _taxCalculatorService
                .CalculateTaxAsync(_mapper.Map<CalculateTaxRequestDTO>(calculateTaxRequest));
        }
    }
}
