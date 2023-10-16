using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Users.LeaseRequestComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Users.LeaseRequestComponent.Handler
{
    public class UpdateLeaseRequestCommandHandler : IRequestHandler<UpdateLeaseRequestCommand, LeaseRequest>
    {
        private readonly ILeaseRequestRepository _leaseRequestRepository;
        private readonly IMapper _mapper;

        public UpdateLeaseRequestCommandHandler(ILeaseRequestRepository leaseRequestRepository, IMapper mapper)
        {
            _leaseRequestRepository = leaseRequestRepository;
            _mapper = mapper;
        }
        public async Task<LeaseRequest> Handle(UpdateLeaseRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseRequestRepository.UpdateLeaseRequest(request.LeaseRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating lease request: " + ex.Message);
            }
        }
    }
}
