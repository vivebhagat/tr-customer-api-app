using AutoMapper;
using ContractRequestSolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Domain.Helper;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Handler
{
    public class UpdateContractRequestCommandHandler : IRequestHandler<UpdateContractRequestCommand, ContractRequest>
    {
        private readonly IContractRequestRepository _contractRequestRepository;
        private readonly IMapper _mapper;
        IHttpHelper _httpHelper;

        public UpdateContractRequestCommandHandler(IContractRequestRepository contractRequestRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _contractRequestRepository = contractRequestRepository;
            _mapper = mapper;
            _httpHelper = httpHelper;
        }
        public async Task<ContractRequest> Handle(UpdateContractRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contractRequestEntity = _mapper.Map<ContractRequest>(request.ContractRequest);
                ContractRequest contractRequest = await _contractRequestRepository.UpdateContractRequest(contractRequestEntity);
                return contractRequest;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating application: " + ex.Message);
            }
        }
    }
}
