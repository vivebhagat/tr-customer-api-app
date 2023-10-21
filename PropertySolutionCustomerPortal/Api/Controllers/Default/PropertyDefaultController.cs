using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Application.Estate.PropertyImageComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Setup;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.EntityFilter.FilterModel;

namespace PropertySolutionCustomerPortal.Api.Controllers.Default
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyDefaultController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyDefaultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetAll")]
        public async Task<List<Property>> GetAllProperties(PropertyFilterUIModel @object)
        {
            return await _mediator.Send(new GetAllPropertiesQuery { PropertyFilterUIModel = @object});
        }

        [HttpGet("[action]")]
        public async Task<List<Property>> GetHomePropertyList()
        {
            return await _mediator.Send(new GetHomeDisplayPropertiesQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<Property> GetPropertyById(int id)
        {
            return await _mediator.Send(new GetPropertyByIdQuery { Id = id });
        }


        [HttpGet("[action]/{id}")]
        public async Task<List<PropertyImage>> GetPropertyImageList(int id)
        {
            return await _mediator.Send(new GetPropertyImageByPropertyIdQuery { Id = id });
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

        [HttpPost("[action]")]
        public async Task<List<Organization>> GetOrganizationDetails(GetOrganizationDetailsQuery @object)
        {
            return await _mediator.Send(new GetOrganizationDetailsQuery { DomainKey = @object.DomainKey });
        }

        [HttpPost("[action]")]
        public async Task<BusinessUser> GetManagerDetails(GetManagerDetailsQuery @obejct)
        {
            return await _mediator.Send(new GetManagerDetailsQuery { Id = @obejct.Id, DomainKey = @obejct.DomainKey });
        }
    }
}