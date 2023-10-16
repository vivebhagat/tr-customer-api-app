using MediatR;
using PropertySolutionCustomerPortal.Api.Dto.Estate;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query
{
    public class GetPropertyByIdQuery : IRequest<Property>
    {
        public int Id { get; set; }
    }
}
