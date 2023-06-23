using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TaxCalculator.Infrastructure.DatabaseContext;
using TaxCalculator.WebApi.MappingProfiles;
using TaxCalculator.Core.MappingProfiles;
using TaxCalculator.Core.ServiceContracts;
using TaxCalculator.Core.Services;
using TaxCalculator.Core.Domain.RepositoryContracts;
using TaxCalculator.Infrastructure.Repositories;

namespace TaxCalculator.Web
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.ConfigureDbContext(builder);
            services.ConfigureAutoMapper();
            services.ConfigureSwagger();
            services.ConfigureControllers();
            services.ConfigureCustomServices();
            services.ConfigureRepositories();
        }

        public static void ConfigureDbContext(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();
        }

        public static void ConfigureCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFlatRatesRepository, FlatRatesRepository>();
            services.AddScoped<IFlatValuesRepository, FlatValuesRepository>();
            services.AddScoped<IPostalCodeCalculationTypesRepository, PostalCodeCalculationTypesRepository>();
            services.AddScoped<IProgressiveRatesRepository, ProgressiveRatesRepository>();
            services.AddScoped<ITaxCalculationResultsRepository, TaxCalculationResultsRepository>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new ViewModelsToDTOsMappingProfile());
                config.AddProfile(new EntitiesToDTOsMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
