using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Query
{
    public class GetAllContractRequestsQuery : IRequest<List<ContractRequest>>
    {
    }
}
