using AutoMapper;
using MediatR;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command;
using PropertySolutionCustomerPortal.Domain.Repository.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Handler
{
    public class SendVerificationEmailCommandHandler : IRequestHandler<SendVerificationEmailCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public SendVerificationEmailCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.SendEmailConfirmationMail(request.UserId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}