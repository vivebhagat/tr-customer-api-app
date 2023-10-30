using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Setup;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;

namespace CommunitySolutionCustomerPortal.Api.Controllers.External
{
    [Route("api/[controller]")]
    [ApiController, ExternalAuthFilter]
    public class CommunityExternalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommunityExternalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddCommunity(CreateCommunityCommand @object)
        {
            return await _mediator.Send(new CreateCommunityCommand { Community = @object.Community });
        }

        [HttpPost("[action]")]
        public async Task<Community> EditCommunity(UpdateCommunityCommand @object)
        {
            return await _mediator.Send(new UpdateCommunityCommand { Community = @object.Community });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteCommunity(int id)
        {
            return await _mediator.Send(new DeleteCommunityCommand { Id = id });
        }
    }
}