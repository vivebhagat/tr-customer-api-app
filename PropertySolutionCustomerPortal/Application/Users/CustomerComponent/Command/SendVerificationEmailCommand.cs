using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command
{
    public class SendVerificationEmailCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}
