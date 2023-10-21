using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Setup;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query
{
    public class GetOrganizationDetailsQuery : IRequest<List<Organization>>
    {
        public string DomainKey { get; set; }
    }
}

