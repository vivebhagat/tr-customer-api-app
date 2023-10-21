using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Setup;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query
{
    public class GetManagerDetailsQuery : IRequest<BusinessUser>
    {
        public int Id { get; set; }
        public string DomainKey { get; set; }
    }
}

