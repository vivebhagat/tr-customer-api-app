using AutoMapper;
using ContractRequestSolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Handler
{
    public class WithdrawContractRequestCommandHandler : IRequestHandler<WithdrawContractRequestCommand, bool>
    {
        private readonly IContractRequestRepository _contractRequestRepository;
        private readonly IMapper _mapper;
        IHttpHelper _httpHelper;

        public WithdrawContractRequestCommandHandler(IContractRequestRepository contractRequestRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _contractRequestRepository = contractRequestRepository;
            _mapper = mapper;
            _httpHelper = httpHelper;
        }
        public async Task<bool> Handle(WithdrawContractRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool isUpdated =  await _contractRequestRepository.WithdrawContractRequest(request.Id);

                if (isUpdated)
                {
                    string postData = JsonConvert.SerializeObject(request);
                    return await _httpHelper.PostAsync<bool>(postData, request.DomainKey, "/api/ContractRequestExternal/WithdrawContractRequest/" );
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating application: " + ex.Message);
            }
        }
    }
}
