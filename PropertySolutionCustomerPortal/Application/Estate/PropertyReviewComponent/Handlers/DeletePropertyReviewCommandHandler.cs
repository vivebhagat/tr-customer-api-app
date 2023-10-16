using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Command;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Users.PropertyReviewComponent.Handler
{
    public class DeletePropertyReviewCommandHandler : IRequestHandler<DeletePropertyReviewCommand, bool>
    {
        private readonly IPropertyReviewRepository _propertyReviewRepository;

        public DeletePropertyReviewCommandHandler(IPropertyReviewRepository propertyReviewRepository)
        {
            _propertyReviewRepository = propertyReviewRepository;
        }

        public async Task<bool> Handle(DeletePropertyReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyReviewRepository.DeletePropertyReview(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty review: " + ex.Message);
            }
        }
    }
}
