using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command
{
    public class CustomerLoginCommand : IRequest<RoleAuthResponse>
    {
        public LoginRequestDto LoginRequestDto { get; set; }
    }
}
