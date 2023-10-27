using MediatR;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
