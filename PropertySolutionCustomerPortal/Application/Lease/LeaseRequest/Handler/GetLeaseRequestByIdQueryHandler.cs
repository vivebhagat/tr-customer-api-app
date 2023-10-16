using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.LeaseRequestComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.LeaseRequestComponent.Handler
{
    public class GetLeaseRequestByIdQueryHandler : IRequestHandler<GetLeaseRequestByIdQuery, LeaseRequest>
    {
        private readonly ILeaseRequestRepository _leaseRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaseRequestByIdQueryHandler(ILeaseRequestRepository leaseRequestRepository, IMapper mapper)
        {
            _leaseRequestRepository = leaseRequestRepository;
            _mapper = mapper;
        }

        public async Task<LeaseRequest> Handle(GetLeaseRequestByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseRequestRepository.GetLeaseRequestById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting lease request: " + ex.Message);
            }
        }
    }
}

