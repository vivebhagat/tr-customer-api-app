using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.LeaseAgreementComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Users.LeaseAgreementComponent.Handler
{
    public class GetAllLeaseAgreementsQueryHandler : IRequestHandler<GetAllLeaseAgreementsQuery, List<LeaseAgreement>>
    {
        private readonly ILeaseAgreementRepository _leaseAgreementRepository;
        private readonly IMapper _mapper;

        public GetAllLeaseAgreementsQueryHandler(ILeaseAgreementRepository leaseAgreementRepository, IMapper mapper)
        {
            _leaseAgreementRepository = leaseAgreementRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaseAgreement>> Handle(GetAllLeaseAgreementsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseAgreementRepository.GetAllLeaseAgreements();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting lease agreement list: " + ex.Message);
            }
        }
    }
}

