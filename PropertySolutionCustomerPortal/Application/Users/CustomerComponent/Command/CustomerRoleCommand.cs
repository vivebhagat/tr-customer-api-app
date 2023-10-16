using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command
{
    public class CustomerRoleCommand : IRequest<RoleAuthResponse>
    {
        public RoleSelectionRequestDto RoleSelectionRequestDto { get; set; }
    }
}
