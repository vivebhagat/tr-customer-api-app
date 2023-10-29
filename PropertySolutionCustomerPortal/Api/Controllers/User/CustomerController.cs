using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using RechordWebApp.Attribute;

namespace PropertySolutionCustomerPortal.Api.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Logout(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("Invalid input parameters.");

            bool result = await _mediator.Send(new CustomerLogoutCommand { UserId = id });

            if (result)
                return Ok(new { result });
            else
                return Unauthorized();
        }

        [HttpPost("[action]")]
        public async Task<Customer> EditCustomer(UpdateCustomerCommand @objec)
        {
            return await _mediator.Send(new UpdateCustomerCommand { Customer = @objec.Customer });
        }

        [HttpGet("[action]/{id}"),Authorize]
        public async Task<bool> DeleteCustomer(string id)
        {
            return await _mediator.Send(new DeleteCustomerCommand { Id = id });
        }

        [HttpGet("GetAll")]
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _mediator.Send(new GetAllCustomersQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<Customer> GetCustomerById(int id)
        {
            return await _mediator.Send(new GetCustomerByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}"), Authorize]
        public async Task<Customer> GetCustomerByUserId(string id)
        {
            return await _mediator.Send(new GetCustomerByUserIdQuery { UserId = id });
        }
    }
}
