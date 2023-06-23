namespace TaxCalculator.Web
{
    public static class WebApplicationConfiguration
    {
        public static void ConfigureWebApplication(this WebApplication application)
        {            
            application.UseRouting();
            application.MapControllers();
            application.ConfigureSwagger();
            application.Run();
        }

        public static void ConfigureSwagger(this WebApplication application)
        {
            application.UseSwagger();

            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API v1");
            });
        }
    }
}
