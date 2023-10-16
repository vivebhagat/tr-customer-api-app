using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public ResetPassword ResetPassword { get; set; }
    }
}
