using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Users.CommunityComponent.Handler
{
    public class DeleteCommunityCommandHandler : IRequestHandler<DeleteCommunityCommand, bool>
    {
        private readonly ICommunityRepository _communityRepository;

        public DeleteCommunityCommandHandler(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }

        public async Task<bool> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityRepository.DeleteCommunity(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty: " + ex.Message);
            }
        }
    }

}
