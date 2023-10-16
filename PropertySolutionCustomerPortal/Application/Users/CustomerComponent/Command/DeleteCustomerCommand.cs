using MediatR;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
