using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.LeaseRequestComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Users.LeaseRequestComponent.Handler
{
    public class GetAllLeaseRequestsQueryHandler : IRequestHandler<GetAllLeaseRequestsQuery, List<LeaseRequest>>
    {
        private readonly ILeaseRequestRepository _leaseRequestRepository;
        private readonly IMapper _mapper;

        public GetAllLeaseRequestsQueryHandler(ILeaseRequestRepository leaseRequestRepository, IMapper mapper)
        {
            _leaseRequestRepository = leaseRequestRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaseRequest>> Handle(GetAllLeaseRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseRequestRepository.GetAllLeaseRequests();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting lease request list: " + ex.Message);
            }
        }
    }
}

