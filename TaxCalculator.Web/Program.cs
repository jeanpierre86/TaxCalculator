using TaxCalculator.Web.ConfigurationOptions;
using TaxCalculator.Web.ServiceContracts;
using TaxCalculator.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.Configure<WebApiConfigurationOptions>
    (builder.Configuration.GetSection("WebApiSettings"));

builder.Services.AddHttpClient();
builder.Services.AddScoped<IWebApiService, WebApiService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.Run();
