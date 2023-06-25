using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Core.DTOs;

namespace TaxCalculator.WebApi.Filters
{
    public class CalculateTaxExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var response = new CalculateTaxResponseDTO
            {
                Errors = new[] { context.Exception.Message }
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
            };

            base.OnException(context);
        }
    }
}
