using AutoMapper;
using ContractRequestSolutionHub.Domain.Repository.Estate;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionCustomerPortal.Domain.Helper;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Handler
{
    public class DeleteContractRequestCommandHandler : IRequestHandler<DeleteContractRequestCommand, bool>
    {
        private readonly IContractRequestRepository _contractRequestRepository;
        private readonly IMapper _mapper;
        IHttpHelper _httpHelper;


        public DeleteContractRequestCommandHandler(IContractRequestRepository contractRequestRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _contractRequestRepository = contractRequestRepository;
            _mapper = mapper;
            _httpHelper = httpHelper;
        }

        public async Task<bool> Handle(DeleteContractRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _contractRequestRepository.DeleteContractRequest(request.Id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating application: " + ex.Message);
            }
        }
    }
}
