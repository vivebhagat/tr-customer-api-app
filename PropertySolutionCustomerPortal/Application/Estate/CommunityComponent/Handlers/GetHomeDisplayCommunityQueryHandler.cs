using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;

namespace PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Handlers
{
    public class GetHomeDisplayPropertiesQueryHandler : IRequestHandler<GetHomeDisplayCommunitiesQuery, List<Community>>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IMapper _mapper;

        public GetHomeDisplayPropertiesQueryHandler(ICommunityRepository communityRepository, IMapper mapper)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
        }
        public async Task<List<Community>> Handle(GetHomeDisplayCommunitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityRepository.GetAllCommunities();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting community: " + ex.Message);
            }
        }
    }
}