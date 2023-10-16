using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command
{
    public class UpdatePropertyCommand : IRequest<Property>
    {
        public PropertyDto Property { get; set; }

    }
}
