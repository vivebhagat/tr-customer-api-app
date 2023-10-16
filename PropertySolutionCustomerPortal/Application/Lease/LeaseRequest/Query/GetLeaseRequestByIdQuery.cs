using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;

namespace PropertySolutionCustomerPortal.Application.Estate.LeaseRequestComponent.Query
{
    public class GetLeaseRequestByIdQuery : IRequest<LeaseRequest>
    {
        public int Id { get; set; }
    }
}
