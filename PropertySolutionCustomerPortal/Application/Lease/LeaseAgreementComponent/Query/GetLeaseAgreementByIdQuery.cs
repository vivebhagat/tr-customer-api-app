using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;

namespace PropertySolutionCustomerPortal.Application.Estate.LeaseAgreementComponent.Query
{
    public class GetLeaseAgreementByIdQuery : IRequest<LeaseAgreement>
    {
        public int Id { get; set; }
    }
}
