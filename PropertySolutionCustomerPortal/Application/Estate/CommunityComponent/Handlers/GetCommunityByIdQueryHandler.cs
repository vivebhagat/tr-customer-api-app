using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;

namespace PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Handlers
{
    public class GetCommunityByIdQueryHandler : IRequestHandler<GetCommunityByIdQuery, Community>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IMapper _mapper;

        public GetCommunityByIdQueryHandler(ICommunityRepository communityRepository, IMapper mapper)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
        }

        public async Task<Community> Handle(GetCommunityByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var proeprtyData =  await _communityRepository.GetCommunityById(request.Id);

            //    CreateCommunityCommand data = new CreateCommunityCommand();
           //     data.Community = communityEntity;

                return proeprtyData;

            }
            catch (Exception ex)
            {
                throw new Exception("Error getting community: " + ex.Message);
            }
        }
    }
}