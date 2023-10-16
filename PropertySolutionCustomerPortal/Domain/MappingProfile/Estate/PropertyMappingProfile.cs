using AutoMapper;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Domain.MappingProfile.Estate
{
    public class PropertyMappingProfile : Profile
    {
        public PropertyMappingProfile()
        {
            CreateMap<Property, PropertyDto>();
            CreateMap<PropertyDto, Property>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RemoteId))
                .ForMember(dest => dest.RemoteId, opt => opt.MapFrom(_ => 0));
        }
    }
}