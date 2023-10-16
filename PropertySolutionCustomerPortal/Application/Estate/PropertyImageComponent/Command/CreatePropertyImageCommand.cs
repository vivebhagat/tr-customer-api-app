using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Command
{
    public class CreatePropertyImageCommand : IRequest<int>
    {
        public PropertyImage PropertyImage { get; set; }

    }
}
