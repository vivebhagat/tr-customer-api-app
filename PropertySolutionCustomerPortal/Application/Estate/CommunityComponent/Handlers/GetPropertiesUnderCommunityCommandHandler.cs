using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Handlers
{
    public class GetPropertiesUnderCommunityCommandHandler : IRequestHandler<GetPropertiesUnderCommunityQuery, List<Property>>
    {
        private readonly ICommunityRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetPropertiesUnderCommunityCommandHandler(ICommunityRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<Property>> Handle(GetPropertiesUnderCommunityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.GetPropertiesUnderCommunity(request.Id, request.DomainKey);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property list: " + ex.Message);
            }
        }
    }
}
