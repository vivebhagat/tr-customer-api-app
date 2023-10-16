using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Handler
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Property>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<Property> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var propertyEntity = _mapper.Map<Property>(request.Property);
                Property property = await _propertyRepository.UpdateProperty(propertyEntity);
                return property;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating proeprty: " + ex.Message);
            }
        }
    }
}
