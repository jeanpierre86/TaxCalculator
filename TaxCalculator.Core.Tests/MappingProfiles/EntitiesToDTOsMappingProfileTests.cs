using AutoMapper;
using TaxCalculator.Core.MappingProfiles;

namespace TaxCalculator.Core.Tests.MappingProfiles
{
    public class EntitiesToDTOsMappingProfileTests
    {
        [Test]
        public void EntitiesToDTOsMappingProfile_ShouldBeValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntitiesToDTOsMappingProfile>();
            });

            // Act & Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}