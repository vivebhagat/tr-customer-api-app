using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Handler
{
    public class GetAllPropertyImagesQueryHandler : IRequestHandler<GetAllPropertyImagesQuery, List<PropertyImage>>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;

        public GetAllPropertyImagesQueryHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
        {
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyImage>> Handle(GetAllPropertyImagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyImageRepository.GetAllPropertyImages();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property image list: " + ex.Message);
            }
        }
    }
}

