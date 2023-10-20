using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PropertySolutionCustomerPortal.Api.Dto.Auth;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using System;
using System.Xml.Linq;

namespace PropertySolutionCustomerPortal.Api.Controllers.Default
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDefaultController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<BaseApplicationUser> _userManager;


        public CustomerDefaultController(IMediator mediator, UserManager<BaseApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Invalid input parameters.");

            LoginRequestDto loginRequestDto = new() { UserName = username, PassWord = password };

            RoleAuthResponse result = await _mediator.Send(new CustomerLoginCommand { LoginRequestDto = loginRequestDto });

            if (result != null)
                return Ok(new { result });
            else
                return Unauthorized();
        }


        [HttpPost("[action]")]
        public async Task<int> AddCustomer(CreateCustomerCommand @object)
        {
            return await _mediator.Send(new CreateCustomerCommand { Customer = @object.Customer });
        }

        [HttpGet("[action]/{id}")]
        public async Task<Customer> GetCustomerById(int id)
        {
            return await _mediator.Send(new GetCustomerByIdQuery { Id = id });
        }
    }
}