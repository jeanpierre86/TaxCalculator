using AutoMapper;
using TaxCalculator.Core.DTOs;
using TaxCalculator.WebApi.ViewModels;

namespace TaxCalculator.WebApi.MappingProfiles
{
    public class ViewModelsToDTOsMappingProfile : Profile
    {
        public ViewModelsToDTOsMappingProfile()
        {
            CreateMap<CalculateTaxRequest, CalculateTaxRequestDTO>();
        }
    }
}
