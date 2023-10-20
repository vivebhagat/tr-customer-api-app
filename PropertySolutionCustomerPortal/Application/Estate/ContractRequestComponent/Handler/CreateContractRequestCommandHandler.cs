using AutoMapper;
using ContractRequestSolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Handler
{
    public class CreateContractRequestCommandHandler : IRequestHandler<CreateContractRequestCommand, int>
    {
        private readonly IContractRequestRepository _contractRequestRepository;
        private readonly IMapper _mapper;
        IHttpHelper _httpHelper;

        public CreateContractRequestCommandHandler(IContractRequestRepository contractRequestRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _contractRequestRepository = contractRequestRepository;
            _mapper = mapper;
            _httpHelper = httpHelper;
        }

        public async Task<int> Handle(CreateContractRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contractRequestEntity = _mapper.Map<ContractRequest>(request.ContractRequest);
                int contractRequestId = await _contractRequestRepository.CreateContractRequest(contractRequestEntity);

                if (contractRequestId != 0)
                {
                    bool emailSent = await _contractRequestRepository.SendNewApplicationEmail(contractRequestId, request.CustomerId);

                    if (emailSent)
                    {
                        string postData = JsonConvert.SerializeObject(request);
                        bool result = await _contractRequestRepository.AddRemoteContractRequest(postData, request.DomainKey, contractRequestId);
                    }
                }

                return contractRequestId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating application: " + ex.Message);
            }
        }
    }
}
