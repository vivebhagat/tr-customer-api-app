using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;
using PropertySolutionCustomerPortal.Domain.Repository.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Handler
{
    public class EmailConfrimCommandHandler : IRequestHandler<EmailConfirmationCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public EmailConfrimCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.EmailConfirmation(request.EmailConfirmation);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property: " + ex.Message);
            }
        }
    }
}