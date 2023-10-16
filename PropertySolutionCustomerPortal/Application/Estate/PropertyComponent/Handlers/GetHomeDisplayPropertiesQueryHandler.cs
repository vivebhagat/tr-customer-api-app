using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Handlers
{
    public class GetHomeDisplayPropertiesQueryHandler : IRequestHandler<GetHomeDisplayPropertiesQuery, List<Property>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetHomeDisplayPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<Property>> Handle(GetHomeDisplayPropertiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.GetHomeDisplayProperties();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property: " + ex.Message);
            }
        }
    }
}