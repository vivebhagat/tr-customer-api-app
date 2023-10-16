using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Application.Estate.LeaseAgreementComponent.Query
{
    public class GetAllLeaseAgreementsQuery : IRequest<List<LeaseAgreement>>
    {
    }
}
