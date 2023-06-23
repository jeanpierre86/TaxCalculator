using AutoMapper;
using TaxCalculator.Core.MappingProfiles;

namespace TaxCalculator.Core.Tests.MappingProfiles
{
    public class DTOsToDTOsMappingProfileTests
    {
        [Test]
        public void DTOsToDTOsMappingProfile_ShouldBeValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DTOsToDTOsMappingProfile>();
            });

            // Act & Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}