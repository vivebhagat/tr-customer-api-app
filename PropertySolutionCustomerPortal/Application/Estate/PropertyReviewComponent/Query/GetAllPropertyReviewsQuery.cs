using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Query
{
    public class GetAllPropertyReviewsQuery : IRequest<List<PropertyReview>>
    {
    }
}
