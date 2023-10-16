using AutoMapper;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Domain.MappingProfile.Estate
{
    public class ContractRequestMappingProfile : Profile
    {
        public ContractRequestMappingProfile()
        {
            CreateMap<ContractRequestDto, ContractRequest>();
            CreateMap<ContractRequest, ContractRequestDto>();
        }
    }
}