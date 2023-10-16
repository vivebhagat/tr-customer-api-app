

using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Command;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Handler
{
    public class DeletePropertyImageCommandHandler : IRequestHandler<DeletePropertyImageCommand, bool>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;

        public DeletePropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository)
        {
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<bool> Handle(DeletePropertyImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyImageRepository.DeletePropertyImage(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty image: " + ex.Message);
            }
        }
    }
}
