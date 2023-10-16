using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.LeaseAgreementComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.LeaseAgreementComponent.Handler
{
    public class GetLeaseAgreementByIdQueryHandler : IRequestHandler<GetLeaseAgreementByIdQuery, LeaseAgreement>
    {
        private readonly ILeaseAgreementRepository _leaseAgreementRepository;
        private readonly IMapper _mapper;

        public GetLeaseAgreementByIdQueryHandler(ILeaseAgreementRepository leaseAgreementRepository, IMapper mapper)
        {
            _leaseAgreementRepository = leaseAgreementRepository;
            _mapper = mapper;
        }

        public async Task<LeaseAgreement> Handle(GetLeaseAgreementByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseAgreementRepository.GetLeaseAgreementById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting lease agreement: " + ex.Message);
            }
        }
    }
}

