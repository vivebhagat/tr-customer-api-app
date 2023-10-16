using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Application.Users.CustomerToRoleMapComponent.Query
{
    public class GetCustomerRolesByUserIdQuery : IRequest<List<CustomerToRoleMap>>
    {
        public string UserId { get; set; }
    }
}