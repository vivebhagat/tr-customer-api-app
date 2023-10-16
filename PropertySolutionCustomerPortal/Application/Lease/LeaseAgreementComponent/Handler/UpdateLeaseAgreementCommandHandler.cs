using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Users.LeaseAgreementComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Users.LeaseAgreementComponent.Handler
{
    public class UpdateLeaseAgreementCommandHandler : IRequestHandler<UpdateLeaseAgreementCommand, LeaseAgreement>
    {
        private readonly ILeaseAgreementRepository _leaseAgreementRepository;
        private readonly IMapper _mapper;

        public UpdateLeaseAgreementCommandHandler(ILeaseAgreementRepository leaseAgreementRepository, IMapper mapper)
        {
            _leaseAgreementRepository = leaseAgreementRepository;
            _mapper = mapper;
        }
        public async Task<LeaseAgreement> Handle(UpdateLeaseAgreementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseAgreementRepository.UpdateLeaseAgreement(request.LeaseAgreement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating lease agreement: " + ex.Message);
            }
        }
    }
}
