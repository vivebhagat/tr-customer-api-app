using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.EntityFilter.FilterModel;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query
{
    public class GetAllCommunityQuery : IRequest<List<Community>>
    {
        public Community Community { get; set; }
    }
}
