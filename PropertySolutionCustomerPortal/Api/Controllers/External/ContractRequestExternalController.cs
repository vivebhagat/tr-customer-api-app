using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;

namespace PropertySolutionCustomerPortal.Api.Controllers.External
{
    [Route("api/[controller]")]
    [ApiController, ExternalAuthFilter]
    public class ContractRequestExternalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContractRequestExternalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteContractRequest(int id)
        {
            return await _mediator.Send(new DeleteContractRequestCommand { Id = id });
        }

        [HttpPost("[action]")]
        public async Task<ContractRequest> EditContractRequest(UpdateContractRequestCommand @object)
        {
            return await _mediator.Send(new UpdateContractRequestCommand { ContractRequest = @object.ContractRequest });
        }

    }
}