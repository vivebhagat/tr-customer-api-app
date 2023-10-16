using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Users.LeaseAgreementComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Users.LeaseAgreementComponent.Handler
{
    public class DeleteLeaseAgreementCommandHandler : IRequestHandler<DeleteLeaseAgreementCommand, bool>
    {
        private readonly ILeaseAgreementRepository _leaseAgreementRepository;
        private readonly IMapper _mapper;

        public DeleteLeaseAgreementCommandHandler(ILeaseAgreementRepository leaseAgreementRepository, IMapper mapper)
        {
            _leaseAgreementRepository = leaseAgreementRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteLeaseAgreementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseAgreementRepository.DeleteLeaseAgreement(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating lease agreement: " + ex.Message);
            }
        }
    }
}
