using MediatR;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command;
using PropertySolutionCustomerPortal.Domain.Repository.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Handler
{
    public class CustomerLogoutCommandHandler : IRequestHandler<CustomerLogoutCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerLogoutCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(CustomerLogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.Logout(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
