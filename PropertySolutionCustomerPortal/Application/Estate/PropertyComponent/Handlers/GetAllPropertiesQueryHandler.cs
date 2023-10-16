using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Handler
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, List<Property>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<Property>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.GetAllProperties(request.PropertyFilterUIModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property list: " + ex.Message);
            }
        }
    }
}

