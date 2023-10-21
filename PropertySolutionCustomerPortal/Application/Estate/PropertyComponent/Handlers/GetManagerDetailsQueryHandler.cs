using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Setup;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Handlers
{
    public class GetManagerDetailsQueryHandler : IRequestHandler<GetManagerDetailsQuery, BusinessUser>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetManagerDetailsQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<BusinessUser> Handle(GetManagerDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.GetManagerDetails(request.Id, request.DomainKey);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting organization details: " + ex.Message);
            }
        }
    }
}