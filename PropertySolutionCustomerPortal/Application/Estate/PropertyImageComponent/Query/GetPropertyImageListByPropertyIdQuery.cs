using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Query
{
    public class GetPropertyImageByPropertyIdQuery : IRequest<List<PropertyImage>>
    {
        public int Id { get; set; }
    }
}
