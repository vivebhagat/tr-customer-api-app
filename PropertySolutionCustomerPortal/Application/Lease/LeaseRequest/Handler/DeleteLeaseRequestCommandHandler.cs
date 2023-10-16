using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Users.LeaseRequestComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Users.LeaseRequestComponent.Handler
{
    public class DeleteLeaseRequestCommandHandler : IRequestHandler<DeleteLeaseRequestCommand, bool>
    {
        private readonly ILeaseRequestRepository _leaseRequestRepository;
        private readonly IMapper _mapper;

        public DeleteLeaseRequestCommandHandler(ILeaseRequestRepository leaseRequestRepository, IMapper mapper)
        {
            _leaseRequestRepository = leaseRequestRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteLeaseRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseRequestRepository.DeleteLeaseRequest(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating lease request: " + ex.Message);
            }
        }
    }
}
