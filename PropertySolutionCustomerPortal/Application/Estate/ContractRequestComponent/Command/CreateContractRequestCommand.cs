using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command
{
    public class CreateContractRequestCommand : IRequest<int>
    {
        public ContractRequestDto ContractRequest { get; set; }
        public string DomainKey { get; set; }
        public int CustomerId { get; set; }

    }
}
