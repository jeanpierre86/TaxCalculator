using TaxCalculator.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder);

var app = builder.Build();
app.ConfigureWebApplication();