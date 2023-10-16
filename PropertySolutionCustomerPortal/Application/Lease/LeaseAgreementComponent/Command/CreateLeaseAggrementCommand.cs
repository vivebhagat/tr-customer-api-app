using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;

namespace PropertySolutionCustomerPortal.Application.Estate.LeaseAgreementComponent.Command
{
    public class CreateLeaseAgreementCommand : IRequest<int>
    {
        public LeaseAgreement LeaseAgreement { get; set; }
    }
}
