using MediatR;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Query
{
    public class GetAllPropertyImagesQuery : IRequest<List<PropertyImage>>
    {
    }
}
