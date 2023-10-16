using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Handler
{
    public class GetCustomerByUserIdQueryHandler : IRequestHandler<GetCustomerByUserIdQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByUserIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Customer> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.GetCustomerByUserId(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting customer: " + ex.Message);
            }
        }
    }
}

