using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Application.Estate.CommunityComponent.Query;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Setup;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.EntityFilter.FilterModel;

namespace PropertySolutionCustomerPortal.Api.Controllers.Default
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityDefaultController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommunityDefaultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<List<Community>> GetAllCommunity()
        {
            return await _mediator.Send(new GetAllCommunityQuery());
        }

        [HttpGet("[action]")]
        public async Task<List<Community>> GetHomeCommunityList()
        {
            return await _mediator.Send(new GetHomeDisplayCommunitiesQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<Community> GetCommunityById(int id)
        {
            return await _mediator.Send(new GetCommunityByIdQuery { Id = id });
        }

        [HttpPost("[action]")]
        public async Task<List<Property>> GetPropertiesUnderCommunity(GetPropertiesUnderCommunityQuery @bject)
        {
            return await _mediator.Send(new GetPropertiesUnderCommunityQuery { Id = @bject.Id, DomainKey = @bject.DomainKey });
        }
    }
}