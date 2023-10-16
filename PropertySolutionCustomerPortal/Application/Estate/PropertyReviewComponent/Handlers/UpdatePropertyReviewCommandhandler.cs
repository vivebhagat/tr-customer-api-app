using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Handlers
{

    public class UpdatePropertyReviewCommandHandler : IRequestHandler<UpdatePropertyReviewCommand, PropertyReview>
    {
        private readonly IPropertyReviewRepository _propertyRepository;

        public UpdatePropertyReviewCommandHandler(IPropertyReviewRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<PropertyReview> Handle(UpdatePropertyReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.UpdatePropertyReview(request.PropertyReview);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating proeprty review: " + ex.Message);
            }
        }
    }
}