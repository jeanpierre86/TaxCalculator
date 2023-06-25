using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Web.ServiceContracts;
using TaxCalculator.Web.ViewModels;

namespace TaxCalculator.Web.Controllers
{
    public class TaxCalculatorController : Controller
    {
        private readonly IWebApiService _webApiService;

        public TaxCalculatorController(IWebApiService webApiService)
        {
            _webApiService = webApiService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View(new CalculateTaxRequest());
        }

        [HttpPost]
        [Route("/calculatetax")]
        public async Task<IActionResult> GetIncomeTax([FromBody]CalculateTaxRequest calculateTaxRequest)
        {
            if (ModelState.IsValid)
            {
                var incomeTax = await _webApiService.GetTaxCalculationAsync(calculateTaxRequest);
                return Json(incomeTax);
            }
            return BadRequest();
        }
    }
}
