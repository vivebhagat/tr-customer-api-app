using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Users.CommunityComponent.Handler
{
    public class CreateCommunityCommandHandler : IRequestHandler<CreateCommunityCommand, int>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IMapper _mapper;
        IHttpHelper httpHelper;

        public CreateCommunityCommandHandler(ICommunityRepository communityRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
            this.httpHelper = httpHelper;
        }

        public async Task<int> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var communityEntity = _mapper.Map<Community>(request.Community);
                int communityId = await _communityRepository.CreateCommunity(communityEntity);
                return communityId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating community: " + ex.Message);
            }
        }
    }
}