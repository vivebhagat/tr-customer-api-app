using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Query
{
    public class GetCustomerByUserIdQuery : IRequest<Customer>
    {
        public string UserId { get; set; }
    }
}
