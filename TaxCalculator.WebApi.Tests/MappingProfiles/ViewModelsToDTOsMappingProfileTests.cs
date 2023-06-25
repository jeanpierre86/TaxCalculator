using AutoMapper;
using TaxCalculator.WebApi.MappingProfiles;

namespace TaxCalculator.WebApi.Tests.MappingProfiles
{
    internal class ViewModelsToDTOsMappingProfileTests
    {
        [Test]
        public void ViewModelsToDTOsMappingProfile_ShouldBeValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelsToDTOsMappingProfile>();
            });

            // Act & Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
