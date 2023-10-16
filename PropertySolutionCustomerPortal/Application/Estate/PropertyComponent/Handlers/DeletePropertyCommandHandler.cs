using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;


namespace PropertySolutionCustomerPortal.Application.Users.PropertyComponent.Handler
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, bool>
    {
        private readonly IPropertyRepository _propertyRepository;

        public DeletePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.DeleteProperty(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty: " + ex.Message);
            }
        }
    }

}
