using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Query
{
    public class GetPropertiesUnderCommunityQuery : IRequest<List<Property>>
    {
        public int Id { get; set; }
        public string DomainKey { get; set; }
    }
}

