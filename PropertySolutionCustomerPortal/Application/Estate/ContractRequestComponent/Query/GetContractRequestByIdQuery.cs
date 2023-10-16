using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Query
{
    public class GetContractRequestByIdQuery : IRequest<ContractRequest>
    {
        public int Id { get; set; }
    }
}
