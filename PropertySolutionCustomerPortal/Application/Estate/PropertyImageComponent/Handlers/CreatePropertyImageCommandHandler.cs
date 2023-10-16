using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Command;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Handler
{
    public class CreatePropertyImageCommandHandler : IRequestHandler<CreatePropertyImageCommand, int>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;

        public CreatePropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository)
        {
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<int> Handle(CreatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyImageRepository.CreatePropertyImage(request.PropertyImage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty image: " + ex.Message);
            }
        }
    }
}
