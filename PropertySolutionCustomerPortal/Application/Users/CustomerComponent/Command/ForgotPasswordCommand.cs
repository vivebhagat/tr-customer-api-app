using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command
{
    public class ForgotPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}

