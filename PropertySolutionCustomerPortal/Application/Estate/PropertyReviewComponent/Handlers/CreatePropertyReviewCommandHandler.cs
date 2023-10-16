using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Command;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Users.PropertyReviewComponent.Handler
{
    public class CreatePropertyReviewCommandHandler : IRequestHandler<CreatePropertyReviewCommand, int>
    {
        private readonly IPropertyReviewRepository _propertyReviewRepository;

        public CreatePropertyReviewCommandHandler(IPropertyReviewRepository propertyReviewRepository)
        {
            _propertyReviewRepository = propertyReviewRepository;
        }

        public async Task<int> Handle(CreatePropertyReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyReviewRepository.CreatePropertyReview(request.PropertyReview);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty review: " + ex.Message);
            }
        }
    }
}
