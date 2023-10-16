using MediatR;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command
{
    public class DeleteContractRequestCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string DomainKey { get; set; }

    }
}
