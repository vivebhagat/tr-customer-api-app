using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.User;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public CustomerDto Customer { get; set; }
    }
}
