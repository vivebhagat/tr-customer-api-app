using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Handler
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customerEntity = _mapper.Map<Customer>(request.Customer);
                return await _customerRepository.CreateCustomer(customerEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating customer: " + ex.Message);
            }
        }
    }
}
