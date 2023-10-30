using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Handler
{
    public class UpdateCommunityCommandHandler : IRequestHandler<UpdateCommunityCommand, Community>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IMapper _mapper;

        public UpdateCommunityCommandHandler(ICommunityRepository communityRepository, IMapper mapper)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
        }

        public async Task<Community> Handle(UpdateCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var communityEntity = _mapper.Map<Community>(request.Community);
                Community community = await _communityRepository.UpdateCommunity(communityEntity);
                return community;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating proeprty: " + ex.Message);
            }
        }
    }
}
