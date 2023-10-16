using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Command
{
    public class UpdatePropertyReviewCommand : IRequest<PropertyReview>
    {
        public PropertyReview PropertyReview { get; set; }

    }
}
