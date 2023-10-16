using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command
{
    public class WithdrawContractRequestCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string DomainKey { get; set; }
    }
}