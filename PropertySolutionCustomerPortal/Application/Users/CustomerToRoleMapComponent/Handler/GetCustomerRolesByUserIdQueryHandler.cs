using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Users.CustomerToRoleMapComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Repository.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerToRoleMapComponent.Handler
{
    public class GetCustomerRolesByUserIdQueryHandler : IRequestHandler<GetCustomerRolesByUserIdQuery, List<CustomerToRoleMap>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerRolesByUserIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerToRoleMap>> Handle(GetCustomerRolesByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.GetCustomerRoleByUserId(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting customer roles: " + ex.Message);
            }
        }
    }
}