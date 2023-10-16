using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.EntityFilter.FilterModel;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query
{
    public class GetAllPropertiesQuery : IRequest<List<Property>>
    {
        public PropertyFilterUIModel PropertyFilterUIModel { get; set; }
    }
}
