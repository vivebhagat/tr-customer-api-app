using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command
{
    public class EmailConfirmationCommand : IRequest<bool>
    {
        public EmailConfirmation EmailConfirmation { get; set; }

    }
}
