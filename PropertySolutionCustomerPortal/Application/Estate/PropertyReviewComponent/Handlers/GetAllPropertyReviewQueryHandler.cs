using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Users.PropertyReviewComponent.Handler
{
    public class GetAllPropertyReviewsQueryHandler : IRequestHandler<GetAllPropertyReviewsQuery, List<PropertyReview>>
    {
        private readonly IPropertyReviewRepository _propertyReviewRepository;
        private readonly IMapper _mapper;

        public GetAllPropertyReviewsQueryHandler(IPropertyReviewRepository propertyReviewRepository, IMapper mapper)
        {
            _propertyReviewRepository = propertyReviewRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyReview>> Handle(GetAllPropertyReviewsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyReviewRepository.GetAllPropertyReviews();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property review list: " + ex.Message);
            }
        }
    }
}

