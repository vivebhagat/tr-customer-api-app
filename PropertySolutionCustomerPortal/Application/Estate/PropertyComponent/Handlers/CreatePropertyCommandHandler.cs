using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;

namespace PropertySolutionCustomerPortal.Application.Users.PropertyComponent.Handler
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, int>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;
        IHttpHelper httpHelper;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper, IPropertyImageRepository propertyImageRepository, IHttpHelper httpHelper)
        {
            _propertyRepository = propertyRepository;
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
            this.httpHelper = httpHelper;
        }

        public async Task<int> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var propertyEntity = _mapper.Map<Property>(request.Property);
                int propertyId = await _propertyRepository.CreateProperty(propertyEntity);
                return propertyId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating property: " + ex.Message);
            }
        }
    }
}