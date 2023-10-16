using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.PropertyReviewComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using RechordWebApp.Attribute;

namespace PropertySolutionCustomerPortal.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class PropertyReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddPropertyReview(PropertyReview property)
        {
            return await _mediator.Send(new CreatePropertyReviewCommand { PropertyReview = property });
        }

        [HttpPost("[action]")]
        public async Task<PropertyReview> EditPropertyReview(PropertyReview property)
        {
            return await _mediator.Send(new UpdatePropertyReviewCommand { PropertyReview = property });
        }

        [HttpGet("GetAll")]
        public async Task<List<PropertyReview>> GetAllProperties()
        {
            return await _mediator.Send(new GetAllPropertyReviewsQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<PropertyReview> GetPropertyReviewById(int id)
        {
            return await _mediator.Send(new GetPropertyReviewsByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeletePropertyReview(int id)
        {
            return await _mediator.Send(new DeletePropertyReviewCommand { Id = id });
        }
    }
}
