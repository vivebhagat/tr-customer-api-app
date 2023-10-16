using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;

namespace PropertySolutionCustomerPortal.Application.Users.LeaseRequestComponent.Command
{
    public class UpdateLeaseRequestCommand : IRequest<LeaseRequest>
    {
        public LeaseRequest LeaseRequest { get; set; }
    }
}
