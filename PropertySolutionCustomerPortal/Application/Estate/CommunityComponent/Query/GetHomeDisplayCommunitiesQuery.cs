using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query
{
    public class GetHomeDisplayCommunitiesQuery : IRequest<List<Community>>
    {
    }
}
