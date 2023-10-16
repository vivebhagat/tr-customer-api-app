using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Handlers
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, Property>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<Property> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var proeprtyData =  await _propertyRepository.GetPropertyById(request.Id);

            //    CreatePropertyCommand data = new CreatePropertyCommand();
           //     data.Property = propertyEntity;

                return proeprtyData;

            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property: " + ex.Message);
            }
        }
    }
}