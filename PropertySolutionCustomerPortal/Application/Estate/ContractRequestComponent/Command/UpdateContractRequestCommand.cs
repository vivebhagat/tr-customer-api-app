using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command
{
    public class UpdateContractRequestCommand : IRequest<ContractRequest>
    {
        public ContractRequestDto ContractRequest { get; set; }
        public string DomainKey { get; set; }
    }
}
