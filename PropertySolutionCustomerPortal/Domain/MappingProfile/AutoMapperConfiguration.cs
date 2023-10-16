using AutoMapper;
using PropertySolutionCustomerPortal.Domain.MappingProfile.Estate;
using PropertySolutionCustomerPortal.Domain.MappingProfile.User;

namespace PropertySolutionCustomerPortal.Domain.MappingProfile
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PropertyMappingProfile>();
                cfg.AddProfile<ContractRequestMappingProfile>();
                cfg.AddProfile<CustomerMappingProfile>();
            });
            return config;
        }
    }
}
