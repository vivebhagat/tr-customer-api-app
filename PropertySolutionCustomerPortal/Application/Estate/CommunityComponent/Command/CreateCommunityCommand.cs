using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Command
{
    public class CreateCommunityCommand : IRequest<int>
    {
        public Community Community { get; set; }
    }
}
