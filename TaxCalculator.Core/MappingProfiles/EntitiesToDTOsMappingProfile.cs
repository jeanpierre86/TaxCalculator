using AutoMapper;
using TaxCalculator.Core.Domain.Entities;
using TaxCalculator.Core.DTOs;

namespace TaxCalculator.Core.MappingProfiles
{
    public class EntitiesToDTOsMappingProfile : Profile
    {
        public EntitiesToDTOsMappingProfile()
        {
            CreateMap<CalculateTaxResponseDTO, TaxCalculationResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.DateLastModified, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));
        }
    }
}
