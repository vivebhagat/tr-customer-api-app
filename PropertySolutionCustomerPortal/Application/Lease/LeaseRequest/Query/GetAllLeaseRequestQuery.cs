using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Application.Estate.LeaseRequestComponent.Query
{
    public class GetAllLeaseRequestsQuery : IRequest<List<LeaseRequest>>
    {
    }
}
