using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Command
{
    public class CreatePropertyReviewCommand : IRequest<int>
    {
        public PropertyReview PropertyReview { get; set; }

    }
}
