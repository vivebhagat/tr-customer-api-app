using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PropertySolutionCustomerPortal.Api.Dto.Auth;
using PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
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

        [HttpPost("[action]")]
        public async Task<IActionResult> EmailConfirmation([FromForm] string emailConfirmation)
        {
            EmailConfirmationCommand @object = JsonConvert.DeserializeObject<EmailConfirmationCommand>(emailConfirmation);

            if (string.IsNullOrWhiteSpace(@object.EmailConfirmation.Email) || string.IsNullOrWhiteSpace(@object.EmailConfirmation.Token))
                return BadRequest("Invalid email confirmation request");

            bool result = await _mediator.Send(new EmailConfirmationCommand { EmailConfirmation = @object.EmailConfirmation });

            if (!result)
                return BadRequest("Invalid email confirmation request");
            return Ok();
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("User not found. Please enter valid email address.");

            bool result = await _mediator.Send(new ForgotPasswordCommand { Email = email });

            if (!result)
                return BadRequest("User not found. Please enter valid email address.");

            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword([FromForm] string resetPassword)
        {
            ResetPasswordCommand @object = JsonConvert.DeserializeObject<ResetPasswordCommand>(resetPassword);

            if (string.IsNullOrWhiteSpace(@object.ResetPassword.Email) || string.IsNullOrWhiteSpace(@object.ResetPassword.Email) || string.IsNullOrWhiteSpace(@object.ResetPassword.Token))
                return BadRequest("Invalid passowrd reset request");

            bool result = await _mediator.Send(new ResetPasswordCommand { ResetPassword = @object.ResetPassword});

            if (!result)
                return BadRequest("Invalid passowrd reset request");
            return Ok();
        }
    }
}