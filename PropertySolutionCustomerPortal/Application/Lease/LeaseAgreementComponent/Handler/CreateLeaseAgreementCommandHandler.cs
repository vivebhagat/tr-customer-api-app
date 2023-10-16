using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.LeaseAgreementComponent.Command;
using PropertySolutionCustomerPortal.Application.Users.LeaseAgreementComponent.Command;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Lease.LeaseAgreementComponent.Handler
{
    public class CreateLeaseAgreementCommandHandler : IRequestHandler<CreateLeaseAgreementCommand, int>
    {
        private readonly ILeaseAgreementRepository _leaseAgreementRepository;
        private readonly IMapper _mapper;

        public CreateLeaseAgreementCommandHandler(ILeaseAgreementRepository leaseAgreementRepository, IMapper mapper)
        {
            _leaseAgreementRepository = leaseAgreementRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaseAgreementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseAgreementRepository.CreateLeaseAgreement(request.LeaseAgreement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating lease agreement: " + ex.Message);
            }
        }
    }
}
