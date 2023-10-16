using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command
{
    public class CreatePropertyCommand : IRequest<int>
    {
        public PropertyDto Property { get; set; }
    }
}
