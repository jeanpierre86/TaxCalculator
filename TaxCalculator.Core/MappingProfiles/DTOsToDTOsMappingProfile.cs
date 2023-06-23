using AutoMapper;
using TaxCalculator.Core.DTOs;

namespace TaxCalculator.Core.MappingProfiles
{
    public class DTOsToDTOsMappingProfile : Profile
    {
        public DTOsToDTOsMappingProfile()
        {
            CreateMap<CalculateTaxRequestDTO, CalculateTaxResponseDTO>()
                .ForMember(dest => dest.IncomeTax, opt => opt.Ignore());
        }
    }
}
