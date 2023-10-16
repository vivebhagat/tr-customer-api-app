using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Handler
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customerEntity = _mapper.Map<Customer>(request.Customer);
                return await _customerRepository.UpdateCustomer(customerEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating customer: " + ex.Message);
            }
        }
    }
}
