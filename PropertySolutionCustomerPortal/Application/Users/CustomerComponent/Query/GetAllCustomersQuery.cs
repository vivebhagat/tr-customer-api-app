using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Query
{
    public class GetAllCustomersQuery : IRequest<List<Customer>>
    {
    }
}
