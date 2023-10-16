using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Handler
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.GetAllCustomers();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting customer list: " + ex.Message);
            }
        }
    }
}

