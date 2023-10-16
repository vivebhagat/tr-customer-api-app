using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Query
{
    public class GetPropertyReviewsByIdQuery : IRequest<PropertyReview>
    {
        public int Id { get; set; }
    }
}
