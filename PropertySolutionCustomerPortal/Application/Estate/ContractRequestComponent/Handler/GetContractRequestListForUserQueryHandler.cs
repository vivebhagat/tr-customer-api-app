using AutoMapper;
using ContractRequestSolutionHub.Domain.Repository.Estate;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Handler
{
    public class GetContractRequestListForUserQueryHandler : IRequestHandler<GetContractRequestListForUserQuery, List<ContractRequest>>
    {
        private readonly IContractRequestRepository _contractRequestRepository;
        private readonly IMapper _mapper;

        public GetContractRequestListForUserQueryHandler(IContractRequestRepository contractRequestRepository, IMapper mapper)
        {
            _contractRequestRepository = contractRequestRepository;
            _mapper = mapper;
        }

        public async Task<List<ContractRequest>> Handle(GetContractRequestListForUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _contractRequestRepository.GetContractRequestListForUser(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting application: " + ex.Message);
            }
        }
    }
}
