using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using RechordWebApp.Attribute;

namespace PropertySolutionCustomerPortal.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class ContractRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContractRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddContractRequest(CreateContractRequestCommand @object)
        {
            return await _mediator.Send(new CreateContractRequestCommand { ContractRequest = @object.ContractRequest, DomainKey = @object.DomainKey, CustomerId = @object.CustomerId });
        }

        [HttpPost("[action]")]
        public async Task<bool> WithdrawContractRequest(WithdrawContractRequestCommand @object)
        {
            return await _mediator.Send(new WithdrawContractRequestCommand { Id = @object.Id, DomainKey = @object.DomainKey });
        }


        [HttpGet("[action]/{id}")]
        public async Task<ContractRequest> GetContractRequestById(int id)
        {
            return await _mediator.Send(new GetContractRequestByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<List<ContractRequest>> GetContractRequestListForUser(int id)
        {
            return await _mediator.Send(new GetContractRequestListForUserQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteContractRequest(int id)
        {
            return await _mediator.Send(new DeleteContractRequestCommand { Id = id });
        }

        [HttpGet("[action]")]
        public IEnumerable<Tuple<int, string>> GetContractRequestStatus()
        {
            return Enum.GetValues(typeof(ContractRequestStatus)).Cast<ContractRequestStatus>().Select(value => new Tuple<int, string>((int)value, Enum.GetName(typeof(ContractRequestStatus), value)));
        }
    }
}
