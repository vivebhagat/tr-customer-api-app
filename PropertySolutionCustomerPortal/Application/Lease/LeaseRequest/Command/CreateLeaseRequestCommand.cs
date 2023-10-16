using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;

namespace PropertySolutionCustomerPortal.Application.Estate.LeaseRequestComponent.Command
{
    public class CreateLeaseRequestCommand : IRequest<int>
    {
        public LeaseRequest LeaseRequest { get; set; }

    }
}
