using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using TaxCalculator.Web.ConfigurationOptions;
using TaxCalculator.Web.ServiceContracts;
using TaxCalculator.Web.ViewModels;

namespace TaxCalculator.Web.Services
{
    public class WebApiService : IWebApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<WebApiConfigurationOptions> webApiConfigurationOptions;

        public WebApiService(
            IHttpClientFactory httpClientFactory,
            IOptions<WebApiConfigurationOptions> webApiConfigurationOptions)
        {
            _httpClientFactory = httpClientFactory ?? 
                throw new ArgumentNullException(nameof(httpClientFactory));

            this.webApiConfigurationOptions = webApiConfigurationOptions ??
                throw new ArgumentNullException(nameof(webApiConfigurationOptions));
        }

        public async Task<CalculateTaxResponse> GetTaxCalculationAsync(CalculateTaxRequest request)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var uri = new Uri($"{webApiConfigurationOptions.Value.BaseUrl}{webApiConfigurationOptions.Value.CalculateTaxUrl}");
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, jsonContent);
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CalculateTaxResponse>(content);
        }
    }
}
