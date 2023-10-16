using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using RechordWebApp.Attribute;

namespace PropertyImageSolutionHub.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class PropertyImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddPropertyImage(PropertyImage property)
        {
            return await _mediator.Send(new CreatePropertyImageCommand { PropertyImage = property });
        }

        [HttpPost("[action]")]
        public async Task<PropertyImage> EditPropertyImage(PropertyImage property)
        {
            return await _mediator.Send(new UpdatePropertyImageCommand { PropertyImage = property });
        }

        [HttpGet("GetAll")]
        public async Task<List<PropertyImage>> GetAllProperties()
        {
            return await _mediator.Send(new GetAllPropertyImagesQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeletePropertyImage(int id)
        {
            return await _mediator.Send(new DeletePropertyImageCommand { Id = id });
        }
    }
}
