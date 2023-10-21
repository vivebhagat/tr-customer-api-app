using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Setup;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;

namespace PropertySolutionCustomerPortal.Api.Controllers.External
{
    [Route("api/[controller]")]
    [ApiController, ExternalAuthFilter]
    public class PropertyExternalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyExternalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddProperty(CreatePropertyCommand @object)
        {
            return await _mediator.Send(new CreatePropertyCommand { Property = @object.Property });
        }

        [HttpPost("[action]")]
        public async Task<Property> EditProperty(UpdatePropertyCommand @object)
        {
            return await _mediator.Send(new UpdatePropertyCommand { Property = @object.Property });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteProperty(int id)
        {
            return await _mediator.Send(new DeletePropertyCommand { Id = id });
        }
    }
}