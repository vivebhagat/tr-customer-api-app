using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Handlers
{
    public class GetPropertyImageByPropertyIdQueryHandler : IRequestHandler<GetPropertyImageByPropertyIdQuery, List<PropertyImage>>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;

        public GetPropertyImageByPropertyIdQueryHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
        {
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyImage>> Handle(GetPropertyImageByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyImageRepository.GetPropertyImageListByPropertyId(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property image: " + ex.Message);
            }
        }
    }
}