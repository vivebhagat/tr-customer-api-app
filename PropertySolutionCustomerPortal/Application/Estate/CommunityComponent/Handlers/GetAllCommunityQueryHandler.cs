using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace CommunitySolutionCustomerPortal.Application.Estate.CommunityComponent.Handler
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllCommunityQuery, List<Community>>
    {
        private readonly ICommunityRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAllPropertiesQueryHandler(ICommunityRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<Community>> Handle(GetAllCommunityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.GetAllCommunities();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property list: " + ex.Message);
            }
        }
    }
}

