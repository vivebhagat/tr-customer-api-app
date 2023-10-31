using AutoMapper;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Domain.MappingProfile.Estate
{
    public class CommunityMappingProfile : Profile
    {
        public CommunityMappingProfile()
        {
            CreateMap<Community, CommunityDto>();
            CreateMap<CommunityDto, Community>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RemoteId))
                .ForMember(dest => dest.RemoteId, opt => opt.MapFrom(_ => 0));
        }
    }
}