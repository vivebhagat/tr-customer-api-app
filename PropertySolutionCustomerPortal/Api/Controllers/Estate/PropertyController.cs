using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Setup;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using RechordWebApp.Attribute;

namespace PropertySolutionCustomerPortal.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("[action]")]
        public IEnumerable<Tuple<int, string>> GetPropertyType()
        {
            return Enum.GetValues(typeof(PropertyType)).Cast<PropertyType>().Select(value => new Tuple<int, string>((int)value, Enum.GetName(typeof(PropertyStatus), value)));
        }
         
        [HttpGet("[action]")]
        public IEnumerable<Tuple<int, string>> GetPropertyStatus()
        {
            return Enum.GetValues(typeof(PropertyStatus)).Cast<PropertyStatus>().Select(value => new Tuple<int, string>((int)value, Enum.GetName(typeof(PropertyStatus), value)));
        }
    }
}
