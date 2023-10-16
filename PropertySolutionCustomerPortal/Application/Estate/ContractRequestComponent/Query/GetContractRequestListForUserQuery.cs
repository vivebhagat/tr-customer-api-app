using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Query
{
    public class GetContractRequestListForUserQuery : IRequest<List<ContractRequest>>
    {
        public int Id { get; set; }
    }
}
