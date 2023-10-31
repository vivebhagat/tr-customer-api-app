using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Command
{
    public class CreateCommunityCommand : IRequest<int>
    {
        public CommunityDto Community { get; set; }
    }
}
