using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Setup;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Handlers
{
    public class GetOrganizationDetailsQueryHandler : IRequestHandler<GetOrganizationDetailsQuery, List<Organization>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetOrganizationDetailsQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<Organization>> Handle(GetOrganizationDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.GetOrganizationDetails(request.DomainKey);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting organization details: " + ex.Message);
            }
        }
    }
}