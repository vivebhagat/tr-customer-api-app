using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Command
{
    public class UpdatePropertyImageCommand : IRequest<PropertyImage>
    {
        public PropertyImage PropertyImage { get; set; }

    }
}
