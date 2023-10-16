using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;

namespace PropertySolutionCustomerPortal.Application.Users.LeaseAgreementComponent.Command
{
    public class UpdateLeaseAgreementCommand : IRequest<LeaseAgreement>
    {
        public LeaseAgreement LeaseAgreement { get; set; }
    }
}
