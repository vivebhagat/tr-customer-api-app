using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command
{
    public class UpdateCommunityCommand : IRequest<bool>
    {
        public CommunityDto Community { get; set; }
    }
}
